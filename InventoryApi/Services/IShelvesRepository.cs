using InventoryApi.Entities;
using InventoryApi.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryApi.Services
{
    public interface IShelvesRepository : IRepository<Shelf>
    {
        PagedResult<Shelf> Get(int pageNumber, int pageSize, string fields, string orderBy, string orderDirection, string searchQuery);
    }
}
