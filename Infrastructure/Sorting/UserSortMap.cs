using System.Linq.Expressions;
using WebApplication10.Entities;

namespace WebApplication10.Infrastructure.Sorting
{
    public class UserSortMap : ISortMap<User>
    {
        public IReadOnlyDictionary<string, Expression<Func<User, object>>> Map { get; } =
            new Dictionary<string, Expression<Func<User, object>>>(StringComparer.OrdinalIgnoreCase)
            {
                ["name"] = x => x.Name,
                ["surname"] = x => x.Surname,
                ["email"] = x => x.Email,
                ["orderNumber"] = x => x.OrderNumber
            };
    }
}
