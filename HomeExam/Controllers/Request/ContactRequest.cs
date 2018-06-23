using System.ComponentModel.DataAnnotations;

namespace HomeExam.Controllers.Request
{
    public class ContactRequest
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
