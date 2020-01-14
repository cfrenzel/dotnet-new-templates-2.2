using System.Threading.Tasks;

namespace SolutionName.Core
{
    public interface IDomainEventDispatcher
    {
        Task Dispatch(IDomainEvent devent);
    }
}
