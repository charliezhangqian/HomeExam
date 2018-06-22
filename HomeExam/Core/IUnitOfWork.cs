using System.Threading.Tasks;

namespace HomeExam.Core
{
    public interface IUnitOfWork
    {
        Task Complete();
    }
}
