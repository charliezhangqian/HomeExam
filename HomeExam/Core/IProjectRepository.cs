using HomeExam.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeExam.Core
{
    public interface IProjectRepository
    {
        Task<IEnumerable<Project>> List();
        void Add(Project project);
    }
}
