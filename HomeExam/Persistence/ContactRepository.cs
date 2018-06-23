using HomeExam.Core;
using HomeExam.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeExam.Persistence
{
    public class ContactRepository : IContactRepository
    {
        private readonly ExamDbContext _dbContext;

        public ContactRepository(ExamDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Contact>> List()
        {
            return await _dbContext.Contacts.ToListAsync();
        }

        public void Add(Contact contact)
        {
            _dbContext.Contacts.Add(contact);
        }

        public async Task<Contact> Get(int id)
        {
            return await _dbContext.Contacts.FindAsync(id);
        }

        public void Remove(Contact contact)
        {
            _dbContext.Contacts.Remove(contact);
        }
    }
}
