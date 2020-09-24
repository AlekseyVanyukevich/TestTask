using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace TestTask.Infrastructure.Tests.Extensions
{
    public static class QueryableExtensions
    {
        public static DbSet<T> BuildMockDbSet<T>(this IQueryable<T> source) where T : class
        {
            var mock = new Mock<DbSet<T>>();
            mock.As<IQueryable<T>>()
                .Setup(x => x.Provider)
                .Returns(new TestAsyncQueryProvider<T>(source.Provider));
            
            mock.As<IQueryable<T>>()
                .Setup(x => x.Expression)
                .Returns(source.Expression);
            
            mock.As<IQueryable<T>>()
                .Setup(x => x.ElementType)
                .Returns(source.ElementType);

            mock.As<IQueryable<T>>()
                .Setup(x => x.GetEnumerator())
                .Returns(source.GetEnumerator());
            
            mock.As<IAsyncEnumerable<T>>()
                .Setup(m => m.GetAsyncEnumerator(default))
                .Returns(new TestAsyncEnumerator<T>(source.GetEnumerator()));

            return mock.Object;

        }
    }
}