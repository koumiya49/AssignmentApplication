using EcommerceApplication.Common.Data;
using EcommerceApplication.Common.Models;
using EcommerceApplication.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework.Internal;

namespace NunitTestRepository
{
    public class productRepositoryUnitTest
    {
        private ProductRepository _ProductRepository;
        private DbContextOptions<assignmentDb> options;
        private Product testProductOne;
        private Product testProductTwo;
        private UnitOfWork unitOfWork;

        [SetUp]
        public void Dependency()
        {
            ProductDeclaration();
            options = new DbContextOptionsBuilder<assignmentDb>()
                .UseInMemoryDatabase(databaseName: "temp_DB").Options;
            assignmentDb context = new assignmentDb(options);
            unitOfWork = new UnitOfWork(context);
            _ProductRepository = new ProductRepository(unitOfWork);
        }
        public void ProductDeclaration()
        {
            testProductOne = new Product()
            {

            };
            testProductTwo = new Product()
            {

            };
        }
        public void ObjectAddToDb()
        {
            using (var Context = new assignmentDb(options))
            {
                Context.products.Add(testProductOne);
                Context.products.Add(testProductTwo);
                Context.SaveChanges();
            }
        }

    }
}