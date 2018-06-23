using System.Collections.Generic;

namespace HomeExam.Core.Models
{
    public class QueryResult<T>
    {
        public int TotalCount { get; set; }
        public IEnumerable<T> Items { get; set; }
    }
}
