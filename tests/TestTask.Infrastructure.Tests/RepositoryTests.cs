using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;
using TestTask.Fixture.Extensions;
using TestTask.Infrastructure.Repositories;
using Xunit;

namespace TestTask.Infrastructure.Tests
{
    public class RepositoryTests
    {
        [Fact]
        public async Task Get_AllBooks_ReturnsData()
        {
            var testObject = new TestClass {Id = 1};
            var testList = new List<TestClass> {testObject};
            var mock = testList.AsQueryable().BuildMockDbSet();
            var context = new Mock<DbContext>();
            context.Setup(x => x.Set<TestClass>()).Returns(mock);
            var repository = new Repository<TestClass>(context.Object);
            var result = await repository.GetAll();
            Assert.Equal(testList, result.ToList());
        }

        [Fact]
        public async Task Get_WithId_ReturnsRecord()
        {
            var testObject = new TestClass();

            var mock = new Mock<DbSet<TestClass>>();
            var context = new Mock<DbContext>();

            context.Setup(x => x.Set<TestClass>()).Returns(mock.Object);
            mock.Setup(x => x.FindAsync(It.IsAny<int>()))
                .ReturnsAsync(testObject);
            var repository = new Repository<TestClass>(context.Object);
            var result = await repository.Get(1);
            context.Verify(x => x.Set<TestClass>());
            mock.Verify(x => x.FindAsync(It.IsAny<int>()));
        }

        [Fact]
        public async Task Add_Record_Should_Success()
        {
            var testObject = new TestClass();

            var mock = new Mock<DbSet<TestClass>>();
            var context = new Mock<DbContext>();

            context.Setup(x => x.Set<TestClass>()).Returns(mock.Object);
            mock.Setup(x =>
                    x.AddAsync(
                        It.IsAny<TestClass>(),
                        It.IsAny<CancellationToken>()
                    )
                )
                .Returns((TestClass model, CancellationToken token) =>
                    new ValueTask<EntityEntry<TestClass>>((EntityEntry<TestClass>) null));

            var repository = new Repository<TestClass>(context.Object);
            await repository.Add(testObject);

            mock.Verify(
                x =>
                    x.AddAsync(It.IsAny<TestClass>(), It.IsAny<CancellationToken>()
                    ),
                Times.Once);
            
            context.Verify(x => x.Set<TestClass>());
        }

        [Fact]
        public void Delete_Record_Should_Success()
        {
            var testObject = new TestClass();
            var mock = new Mock<DbSet<TestClass>>();
            var context = new Mock<DbContext>();
            context.Setup(x => x.Set<TestClass>())
                .Returns(mock.Object);
            mock.Setup(x => x.Remove(It.IsAny<TestClass>()))
                .Verifiable();
            var repository = new Repository<TestClass>(context.Object);
            repository.Delete(testObject);
            
            context.Verify(x => x.Set<TestClass>());
            mock.Verify(x => x.Remove(It.IsAny<TestClass>()), Times.Once);
        }

        [Fact]
        public void Update_Record_Should_Success()
        {
            var testObject = new TestClass();
            var mock = new Mock<DbSet<TestClass>>();
            var context = new Mock<DbContext>();
            context.Setup(x => x.Set<TestClass>())
                .Returns(mock.Object);
            mock.Setup(x => x.Update(It.IsAny<TestClass>()))
                .Verifiable();
            var repository = new Repository<TestClass>(context.Object);
            repository.Update(testObject);
            mock.Verify(x => x.Update(It.IsAny<TestClass>()), Times.Once);
        }
    }
}