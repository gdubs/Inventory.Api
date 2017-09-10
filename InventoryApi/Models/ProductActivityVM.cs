using InventoryApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryApi.Models
{
    public class ProductActivityVM
    {
        public ActivityType ActivityType { get; set; }
        public int ProductId { get; set; }
        public int Total { get; set; }
        public DateTime Date { get; set; }
    }
}
