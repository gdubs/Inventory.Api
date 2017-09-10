using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using InventoryApi.Helpers;

namespace InventoryApi.Controllers
{
    public abstract class BaseController : Controller
    {
        public const string UrlHelper = "UrlHelper";
        private IConfiguration _configuration;
        private IHttpContextAccessor _httpContextAccessor;

        public BaseController(IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            context.HttpContext.Items[UrlHelper] = this.Url;
        }

        public string GeneratePagingLink(ResourceUriPagingType type, GetParameters parameters, string resourceType)
        {
            switch (type)
            {
                case ResourceUriPagingType.NextPage:
                    parameters.pageNumber += 1;
                    break;
                case ResourceUriPagingType.PreviousPage:
                    parameters.pageNumber = parameters.pageNumber > 1 ? parameters.pageNumber - 1 : 0;
                    break;
                default:
                    break;
            }
            var urlHelper = (IUrlHelper)_httpContextAccessor.HttpContext.Items[BaseController.UrlHelper];
            //var resourceName = resourceType.GetType().Name;
            var url = urlHelper.Link($"Get{resourceType}s", parameters);

            return url;
        }
    }
}