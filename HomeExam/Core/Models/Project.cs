using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace HomeExam.Core.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public ICollection<ProjectContact> Contacts { get; set; }

        public Project()
        {
            Contacts = new Collection<ProjectContact>();
        }
    }
}
