using HomeExam.Core;
using System.Threading.Tasks;

namespace HomeExam.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ExamDbContext _dbContext;

        public UnitOfWork(ExamDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Complete()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
