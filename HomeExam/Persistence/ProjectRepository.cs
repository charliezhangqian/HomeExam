using HomeExam.Core;
using HomeExam.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

        public async Task<IEnumerable<Project>> List()
        {
            return await _dbContext.Projects.ToListAsync();
        }

        public void Add(Project project)
        {
            _dbContext.Projects.Add(project);
        }
    }
}
