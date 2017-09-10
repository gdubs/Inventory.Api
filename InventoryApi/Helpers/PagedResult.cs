using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryApi.Helpers
{
    public class PagedResult<T> : List<T>
    {
        public int TotalCount { get; private set; }
        public int PageSize { get; private set; }
        public int CurrentPage { get; private set; }
        public int Totalpages { get; private set; }

        public bool HasPreviousPage { get { return (CurrentPage > 1); } }
        public bool HasNextPage { get { return (CurrentPage < Totalpages); } }

        public PagedResult(List<T> items, int count, int pageNumber, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            Totalpages = (int)Math.Ceiling(count / (double)pageSize);
            AddRange(items);
        }

        public static PagedResult<T> Create(List<T> items, int totalCount, int pageNumber, int pageSize)
        {
            return new PagedResult<T>(items, totalCount, pageNumber, pageSize);
        }
    }
}
