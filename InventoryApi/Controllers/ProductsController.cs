using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using InventoryApi.Services;
using InventoryApi.Models;
using AutoMapper;
using InventoryApi.Entities;
using Microsoft.AspNetCore.JsonPatch;
using InventoryApi.Helpers;
using Newtonsoft.Json;
using InventoryApi.ActionHelpers;

namespace InventoryApi.Controllers
{
    [Route("api/products")]
    public class ProductsController : BaseController
    {
        private IProductsRepository _productRepository;
        private IMapper _mapper;
        //private IUrlHelper _urlHelper;
        private IHttpContextAccessor _httpContextAccessor;

        public ProductsController(IProductsRepository productRepository, 
            IConfiguration configuration,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor) 
            : base(configuration, httpContextAccessor)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        
        /**
        * Get Paged, Filtered and Queried Products
        * **/
        [MetadataFilter(true)]
        [HttpGet(Name ="GetProducts")]
        public IActionResult Get(GetParameters parameters)
        {
            var products = _productRepository.Get(parameters.pageNumber, parameters.pageSize, parameters.Fields, parameters.OrderBy, parameters.OrderDirection, parameters.SearchQuery);

            var metadata = new
            {
                totalCount = products.TotalCount,
                pageSize = products.PageSize,
                currentPage = products.CurrentPage,
                totalPages = products.Totalpages,
                nextPage = products.HasNextPage ? GeneratePagingLink(ResourceUriPagingType.NextPage, parameters, "Product") : null,
                previousPage = products.HasPreviousPage ? GeneratePagingLink(ResourceUriPagingType.PreviousPage, parameters, "Product") : null
            };

            ViewBag.MetadataObjects = metadata;
            
            return Ok(_mapper.Map<IEnumerable<ProductVM>>(products));
        }

        [HttpGet("{id}", Name = "GetProduct")]
        public IActionResult GetProduct(int id)
        {
            var product = _productRepository.GetById(id);

            if (product == null)
                return NotFound();

            return Ok(_mapper.Map<ProductVM>(product));
        }

        [HttpPost]
        public IActionResult New([FromBody] ProductVM product)
        {
            if (product == null)
                return BadRequest();

            var _product = Mapper.Map<Product>(product);
            
            if (_productRepository.New(_product) == 0)
            {
                throw new Exception("Failed creating new Product");
            }
            
            return CreatedAtRoute("GetProduct", new { id = _product.Id }, _product);
        }

        [HttpPatch("{id}")]
        public IActionResult Update(int id, [FromBody] JsonPatchDocument<ProductVM> updatedProduct)
        {
            if(updatedProduct == null){
                return BadRequest();
            }

            var _product = _productRepository.GetById(id);
            if (_product == null)
            {
                return NotFound();
            }

            var _eProductVM = Mapper.Map<ProductVM>(_product);

            updatedProduct.ApplyTo(_eProductVM);

            _mapper.Map(_eProductVM, _product);

            var updated = _productRepository.Update(_product);
            
            return NoContent();
        }

        [HttpDelete("{id}", Name ="DeleteProduct")]
        public IActionResult Delete(int id)
        {
            var _product = _productRepository.GetById(id);
            if(_product == null)
            {
                return NotFound();
            }

            _productRepository.Delete(_product);

            return Ok();
        }

        
    }
}