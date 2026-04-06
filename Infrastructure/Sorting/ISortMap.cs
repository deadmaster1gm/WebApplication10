using System.Linq.Expressions;

namespace WebApplication10.Infrastructure.Sorting
{
    public interface ISortMap<T>
    {
        IReadOnlyDictionary<string, Expression<Func<T, object>>> Map { get; }
    }
}
