using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryApi.Services;
using InventoryApi.Entities;
using InventoryApi.Helpers;

namespace InventoryApi.Services
{
    public interface IProductsRepository : IRepository<Product>, IDisposable
    {
        PagedResult<Product> Get(int pageNumber, int pageSize, string fields, string orderBy, string orderDirection, string searchQuery);
    }
}
