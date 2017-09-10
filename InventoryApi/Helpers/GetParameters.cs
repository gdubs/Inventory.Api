using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryApi.Helpers
{
    public class GetParameters
    {

        const int _maxPageSize = 20;
        public int pageNumber { get; set; } = 1;
        
        private int _pageSize { get; set; } = 10;
        public int pageSize
        {
            get { return _pageSize; }
            set { _pageSize = (value > _maxPageSize) ? _maxPageSize : value; }
        }

        public string SearchQuery { get; set; }

        public string OrderBy { get; set; } = "Id";
        public string OrderDirection { get; set; } = "Asc";

        public string Fields { get; set; }
    }
}
