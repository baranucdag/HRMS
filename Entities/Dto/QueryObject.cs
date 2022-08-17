using System.Collections.Generic;
using System.Linq;

namespace Entities.Dto
{
    public class QueryObject
    {
        public string QueryString { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int TotalCount { get; set; }
        public IQueryable<JobAdvertDto> Items { get; set; }
    }
}
