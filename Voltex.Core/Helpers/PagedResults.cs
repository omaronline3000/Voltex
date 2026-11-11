namespace Voltex.Core.Helper
{
    public class PagedResults<T> where T : class
    {
        public IReadOnlyList<T>? Items { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
    }
}
