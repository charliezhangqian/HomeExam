using HomeExam.Core.Models;
using System.Threading.Tasks;

namespace HomeExam.Core
{
    public interface IContactRepository
    {
        Task<QueryResult<Contact>> Filter(QueryObject queryObj);
        void Add(Contact contact);
        Task<Contact> Get(int id);
        void Remove(Contact contact);
    }
}
