namespace HomeExam.Extensions
{
    public interface IQueryObject
    {
        string Query { get; set; }
        string QueryBy { get; set; }
        string SortBy { get; set; }
        bool IsSortAscending { get; set; }
        int Page { get; set; }
        byte PageSize { get; set; }
    }
}
