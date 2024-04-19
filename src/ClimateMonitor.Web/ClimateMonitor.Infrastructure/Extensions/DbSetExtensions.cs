using ClimateMonitor.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace ClimateMonitor.Infrastructure.Extensions;

public static class DbSetExtensions
{
    public static async Task<T> FindOrThrow<T>(this DbSet<T> dbSet, object id, CancellationToken cancellationToken) where T : class
        => (await dbSet.FindAsync([id], cancellationToken))
            .ThrowIfNull(id);

    public static async Task<T> FindOrThrow<T, TKey>(this IQueryable<T> query, Expression<Func<T, TKey>> idSelector, TKey id, CancellationToken cancellationToken)
        where T : class
        where TKey : notnull
        => (await query.FirstOrDefaultAsync(BuildPredicate(idSelector, id), cancellationToken))
            .ThrowIfNull(id);

    public static async Task<T> FirstOrThrow<T, TKey>(this IQueryable<T> query, TKey id, CancellationToken cancellationToken)
        where T : class
        where TKey : notnull
        => (await query.FirstOrDefaultAsync(cancellationToken))
            .ThrowIfNull(id);

    public static async Task<T> FindOrThrow<T, TProperty, TKey>(this IIncludableQueryable<T, TProperty> query, Expression<Func<T, TKey>> idSelector, TKey id, CancellationToken cancellationToken)
        where T : class
        where TKey : notnull
        => (await query.FirstOrDefaultAsync(BuildPredicate(idSelector, id), cancellationToken))
            .ThrowIfNull(id);

    private static T ThrowIfNull<T, TKey>(this T? entity, TKey id) where T : class where TKey : notnull
        => entity ?? throw new EntityNotFoundException<T>(id);

    private static Expression<Func<T, bool>> BuildPredicate<T, TKey>(Expression<Func<T, TKey>> idSelector, TKey id)
    {
        var parameter = Expression.Parameter(typeof(T));
        var equalsExpr = Expression.Equal(Expression.Invoke(idSelector, parameter), Expression.Constant(id));
        return Expression.Lambda<Func<T, bool>>(equalsExpr, parameter);
    }
}