using System;
using System.Collections.Generic;
using System.Text;

namespace Voltex.Core.Sharing
{
    public class QueryParameters
    {
        // Pagination
        public int PageIndx { get; set; } = 1;
        public int PageSize { get; set; } = 6;

        // Sorting
        public string? SortBy { get; set; }
        public string? SortDirection { get; set; } = SortDirections.ASC;

        //Search
        public string? Search { get; set; }

        //Filter
        public int? CategoryId { get; set; }
        public int? MaxPrice { get; set; }
        public int? MinPrice { get; set; }


    }
}
