using HomeExam.Core;
using HomeExam.Core.Models;
using HomeExam.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public async Task<QueryResult<Contact>> Filter(QueryObject queryObj)
        {
            var queryResult = new QueryResult<Contact>();

            var query = _dbContext.Contacts.AsQueryable();

            if (!string.IsNullOrWhiteSpace(queryObj.Query))
            {
                var queryColumnsMap = new Dictionary<string, Expression<Func<Contact, bool>>>
                {
                    ["name"] = c => c.Name.Contains(queryObj.Query),
                    ["email"] = c => c.Email.Contains(queryObj.Query),
                    ["phone"] = c => c.Phone.Contains(queryObj.Query)
                };

                query = query.Where(queryColumnsMap[queryObj.QueryBy]);
            }

            var columnsMap = new Dictionary<string, Expression<Func<Contact, object>>>
            {
                ["name"] = c => c.Name,
                ["email"] = c => c.Email,
                ["phone"] = c => c.Phone
            };

            query = query.ApplyOrdering(queryObj, columnsMap);

            queryResult.TotalCount = await query.CountAsync();
            query = query.ApplyPaging(queryObj);
            queryResult.Items = await query.ToListAsync();

            return queryResult;
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
