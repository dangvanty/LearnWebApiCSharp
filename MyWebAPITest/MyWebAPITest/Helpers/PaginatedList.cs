namespace MyWebAPITest.Helpers
{
    public class PaginatedList<T>:List<T>
    {
        public int PageIndex { get; set; }
        public int TotalPage { get; set; }
        public PaginatedList(List<T> items, int count, int pageIndex, int PAGE_SIZE)
        {
            PageIndex = pageIndex;
            TotalPage = (int)Math.Ceiling(count/(double)PAGE_SIZE);
            AddRange(items);
        }
        
        
        public static PaginatedList<T> Create(IQueryable<T> source , int pageIndex, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return new PaginatedList<T>(items, count, pageIndex, pageSize);

        }
    }
}
