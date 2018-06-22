using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HomeExam.Controllers.Request
{
    public class ProjectRequest
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }

        public ICollection<int> Contacts { get; set; }

        public ProjectRequest()
        {
            Contacts = new List<int>();
        }
    }
}
