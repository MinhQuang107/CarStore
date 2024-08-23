using Microsoft.EntityFrameworkCore;

namespace CarStore.Areas.Store.Models
{
    public class PaginatedList<T>
    {
        public List<T> Items { get; private set; }
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }

        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            Items = items;
        }

        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;

        public static async Task<PaginatedList<T>> CreateAsync(IEnumerable<T> source, int pageIndex, int pageSize)
        {
            // Convert to IQueryable if not already
            var queryableSource = source.AsQueryable();

            var count = await Task.FromResult(queryableSource.Count());
            var items = await Task.FromResult(queryableSource.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList());

            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }
    }
}
