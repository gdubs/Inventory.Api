using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryApi.Entities
{
    public class ProductActivity
    {
        public int Id { get; set; }
        public ActivityType ActivityType { get; set; }
        public int ProductId { get; set; }
        public int Total { get; set; }
        public int AuditById { get; set; }
        public DateTime Date { get; set; }

        public Product Product { get; set; }
        public Employee AuditBy { get; set; }
    }
}
