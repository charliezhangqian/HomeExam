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
    public class ProjectRepository : IProjectRepository
    {
        private readonly ExamDbContext _dbContext;

        public ProjectRepository(ExamDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<QueryResult<Project>> Filter(QueryObject queryObj)
        {
            var queryResult = new QueryResult<Project>();

            var query = _dbContext.Projects.
                Include(p => p.Contacts).
                ThenInclude(pc => pc.Contact).
                AsQueryable();

            if (!string.IsNullOrWhiteSpace(queryObj.Query))
            {
                query = query.Where(p => p.Name.Contains(queryObj.Query));
            }

            var columnsMap = new Dictionary<string, Expression<Func<Project, object>>>
            {
                ["name"] = p => p.Name,
                ["startDate"] = p => p.StartDate,
                ["endDate"] = p => p.EndDate,
                ["contacts"] = p => p.Contacts.Count
            };

            query = query.ApplyOrdering(queryObj, columnsMap);

            queryResult.TotalCount = await query.CountAsync();
            query = query.ApplyPaging(queryObj);
            queryResult.Items = await query.ToListAsync();

            return queryResult;
        }

        public void Add(Project project)
        {
            _dbContext.Projects.Add(project);
        }

        public async Task<Project> Get(int id)
        {
            return await _dbContext.Projects.Include(x => x.Contacts).ThenInclude(c => c.Contact)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public void Remove(Project project)
        {
            _dbContext.Projects.Remove(project);
        }
    }
}
