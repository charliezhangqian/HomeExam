using HomeExam.Core.Models;
using System.Threading.Tasks;

namespace HomeExam.Core
{
    public interface IProjectRepository
    {
        Task<QueryResult<Project>> Filter(QueryObject queryObj);
        void Add(Project project);
        Task<Project> Get(int id);
        void Remove(Project project);
    }
}
