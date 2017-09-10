using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using InventoryApi.Services;
using AutoMapper;
using InventoryApi.Models;
using InventoryApi.Helpers;
using InventoryApi.ActionHelpers;

namespace InventoryApi.Controllers
{
    [MetadataFilter(true)]
    [Route("api/shelves")]
    public class ShelvesController : BaseController
    {
        private IShelvesRepository _shelvesRepository;
        private IMapper _mapper;

        public ShelvesController(IConfiguration configuration, 
            IShelvesRepository shelvesRepository,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor) : base(configuration, httpContextAccessor)
        {
            _shelvesRepository = shelvesRepository;
            _mapper = mapper;
        }

        [HttpGet(Name = "GetShelves")]
        public IActionResult Get(GetParameters parameters)
        {
            var shelves = _shelvesRepository.Get(parameters.pageNumber, parameters.pageSize, parameters.Fields, parameters.OrderBy, parameters.OrderDirection, parameters.SearchQuery);

            var metadata = new
            {
                totalCount = shelves.TotalCount,
                pageSize = shelves.PageSize,
                currentPage = shelves.CurrentPage,
                totalPages = shelves.Totalpages,
                nextPage = shelves.HasNextPage ? GeneratePagingLink(ResourceUriPagingType.NextPage, parameters, "Shelve") : null,
                previousPage = shelves.HasPreviousPage ? GeneratePagingLink(ResourceUriPagingType.PreviousPage, parameters, "Shelve") : null
            };

            ViewBag.MetadataObjects = metadata;

            return Ok(_mapper.Map<IEnumerable<ShelfVM>>(shelves));
        }

        [HttpGet("{id}", Name = "GetShelf")]
        public IActionResult GetShelf(int id)
        {
            var shelf = _shelvesRepository.GetById(id);

            if (shelf == null)
                return NotFound();

            return Ok(_mapper.Map<ShelfVM>(shelf));
        }
    }
}