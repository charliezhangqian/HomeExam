using System;
using System.Collections.Generic;

namespace HomeExam.Controllers.Response
{
    public class ProjectResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public ICollection<ContactResponse> Contacts { get; set; }

        public ProjectResponse()
        {
            Contacts = new List<ContactResponse>();
        }
    }
}
