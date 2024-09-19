using Shop.Application.Common.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Application.Common.Mappings
{
    public static class MappingExtensions
    {
        public static async Task<PaginatedList<T>> PaginatedListAsync<T>(
            this IQueryable<T> queryable, 
            int pageNumber, 
            int pageSize)
            => await PaginatedList<T>.CreateAsync(queryable, pageNumber, pageSize);
    }
}
