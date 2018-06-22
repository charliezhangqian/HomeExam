namespace HomeExam.Core.Models
{
    public class ProjectContact
    {
        public int ProjectId { get; set; }
        public int ContactId { get; set; }
        public Project Project { get; set; }
        public Contact Contact { get; set; }
    }
}
