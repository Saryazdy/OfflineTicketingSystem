using Microsoft.EntityFrameworkCore;
using OfflineTicketing.Application.Common.Models;

namespace OfflineTicketing.Application.Common.Extensions
{
    public static class IQueryableExtensions
    {
        public static async Task<PaginatedList<T>> ToPaginatedListAsync<T>(
            this IQueryable<T> query,
            int pageNumber,
            int pageSize,   
            CancellationToken cancellationToken = default)
        {
            var count = await query.CountAsync(cancellationToken);

            var items = await query.Skip((pageNumber - 1) * pageSize)
                                   .Take(pageSize)
                                   .ToListAsync(cancellationToken);

            return new PaginatedList<T>(items, count, pageNumber, pageSize);
        }
    }
}
