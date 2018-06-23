using HomeExam.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeExam.Core
{
    public interface IContactRepository
    {
        Task<IEnumerable<Contact>> List();
        void Add(Contact contact);
        Task<Contact> Get(int id);
        void Remove(Contact contact);
    }
}
