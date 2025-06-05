namespace EcommerceBackend.Helpers
{
    public static class PaginationHelper
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> source, int page, int pageSize)
        {
            return source.Skip((page - 1) * pageSize).Take(pageSize);
        }
    }
}
