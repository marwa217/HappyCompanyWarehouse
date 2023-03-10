

namespace HappyCompany.Domain.Filters
{
    public class paginationFilter
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public paginationFilter()
        {
            PageNumber = 1;
            PageSize = 10;
        }
        public paginationFilter(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber < 1 ? 1 : pageNumber;
            PageSize = pageSize > 10 ? 10 : pageSize;
        }
    }
}
