using Microsoft.EntityFrameworkCore;

namespace PhoneBook.TerrenceLGee.Extensions.Pagination;

public static class QueryableExtensions
{
    extension<T>(IQueryable<T> source)
    {
        public async Task<PagedList<T>> ToPagedListAsync(int count, int page, int pageSize)
        {
            if (count > 0)
            {
                var items = await source
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return new PagedList<T>(items, count, page, pageSize);
            }

            return new(Enumerable.Empty<T>(), 0, 0, 0);
        }
    }
}
