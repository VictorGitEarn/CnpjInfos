using CI.Core.DomainObjects;

namespace CI.Core.Data
{
    public interface IRepository<T> where T : IAggregateRoot { }
}
