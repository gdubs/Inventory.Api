using InventoryApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryApi.Models
{
    public class ProductVM
    {
        public string Url { get; set; }
        public string Name { get; set; }
        public string ASIN { get; set; }
        //public int TotalCount { get; set; }
        public int? ShelfId { get; set; }
        public string ShelfCode { get; set; }

        public List<ProductActivityVM> ProductActivities { get; set; }
        public ShelfVM Shelf { get; set; }
    }
}
