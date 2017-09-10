using AutoMapper;
using InventoryApi.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;

namespace InventoryApi.Helpers
{
    public class GetSingleResourceUrlResolver<TSource, TDestination> : IValueResolver<TSource, TDestination, string>
            where TSource : class
            where TDestination : class
    {
        private IHttpContextAccessor _httpContextAccessor;

        public GetSingleResourceUrlResolver(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public string Resolve(TSource source, TDestination destination, string destMember, ResolutionContext context)
        {
            var url = (IUrlHelper)_httpContextAccessor.HttpContext.Items[BaseController.UrlHelper];
            var objectType = source.GetType();
            var idValue = objectType.GetProperty("Id").GetValue(source);
            var className = source.GetType().Name;

            return url.Link($"Get{className}", new { id = idValue });
        }
    }
}
