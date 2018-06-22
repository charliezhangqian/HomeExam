using AutoMapper;
using HomeExam.Controllers.Request;
using HomeExam.Controllers.Response;
using HomeExam.Core.Models;
using System.Linq;

namespace HomeExam.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain to Api model
            CreateMap<Contact, ContactResponse>();
            CreateMap<Project, ProjectResponse>()
                .ForMember(pr => pr.Contacts, opt => opt.MapFrom(p => p.Contacts.Select(c => c.Contact)));

            // Api model to Domain
            CreateMap<ProjectRequest, Project>()
                .ForMember(p => p.Id, opt => opt.Ignore())
                .ForMember(p => p.Contacts, opt => opt.Ignore())
                .AfterMap((pr, p) =>
                {
                    // Remove unselected contacts
                    var removedContacts = p.Contacts.Where(c => !pr.Contacts.Contains(c.ContactId)).ToList();
                    foreach (var c in removedContacts)
                        p.Contacts.Remove(c);

                    // Add new contacts
                    var addContacts = pr.Contacts.Where(id => p.Contacts.All(c => c.ContactId != id))
                        .Select(id => new ProjectContact { ContactId = id }).ToList();
                    foreach (var c in addContacts)
                        p.Contacts.Add(c);
                });
        }
    }
}
