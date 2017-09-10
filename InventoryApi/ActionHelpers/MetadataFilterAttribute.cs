using InventoryApi.Controllers;
using InventoryApi.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryApi.ActionHelpers
{
    public class MetadataFilterAttribute : ActionFilterAttribute
    {
        private bool _paging;

        public MetadataFilterAttribute(bool pagingNeeded = false)
        {
            _paging = pagingNeeded;
        }
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            var baseController = ((BaseController)context.Controller);
            var metadataObjects = baseController.ViewBag.MetadataObjects;

            if (metadataObjects != null) {
                // var objType = 
                /* var obj = metadataObjects typeof(objType);
                 var metadata = new
                 {
                     totalCount = products.TotalCount,
                     pageSize = products.PageSize,
                     currentPage = products.CurrentPage,
                     totalPages = products.Totalpages,
                     nextPage = products.HasNextPage ? baseController.GeneratePagingLink(ResourceUriPagingType.NextPage, parameters) : null,
                     previousPage = products.HasPreviousPage ? baseController.GeneratePagingLink(ResourceUriPagingType.PreviousPage, parameters) : null
                 };*/
                 
                context.HttpContext.Response.Headers["Resource-Metadata"] = JsonConvert.SerializeObject(metadataObjects);
                
            }
            base.OnResultExecuting(context);
        }
    }
}
