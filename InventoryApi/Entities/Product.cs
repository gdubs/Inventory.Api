using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryApi.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string ASIN { get; set; }
       // public int TotalCount { get; set; }
        public int? ShelfId { get; set; }
                
        [Write(false)]
        public virtual Shelf Shelf { get; set; }
        public virtual ICollection<ProductActivity> ProductActivities { get; set; }
    }
}
