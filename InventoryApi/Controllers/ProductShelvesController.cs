using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryApi.Controllers
{
    [Produces("application/json")]
    [Route("api/shelves/{shelfId}/products/{productId}")]
    public class ProductShelvesController : Controller
    {
    }
}