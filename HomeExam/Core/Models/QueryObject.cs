using HomeExam.Extensions;

namespace HomeExam.Core.Models
{
    public class QueryObject : IQueryObject
    {
        public string Query { get; set; }
        public string QueryBy { get; set; }
        public string SortBy { get; set; }
        public bool IsSortAscending { get; set; }
        public int Page { get; set; }
        public byte PageSize { get; set; }
    }
}
