using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryApi.Entities
{
    public class Shelf
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShelfCode { get; set; }

        [Write(false)]
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
