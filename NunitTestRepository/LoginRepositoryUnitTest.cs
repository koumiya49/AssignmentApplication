using EcommerceApplication.Common.Data;
using EcommerceApplication.Common.Models;
using EcommerceApplication.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework.Internal;

namespace NunitTestRepository
{
    [TestFixture]
    public class LoginRepositoryUnitTest
    {
        private LoginRepository _loginRepository;
        private DbContextOptions<assignmentDb> options;
        private Users testUserOne;
        private Users testUserTwo;
        private UnitOfWork unitOfWork;

        [SetUp]
        public void Dependency()
        {
            UserDeclaration();
            options = new DbContextOptionsBuilder<assignmentDb>()
                .UseInMemoryDatabase(databaseName: "temp_DB").Options;
            assignmentDb context = new assignmentDb(options);
            unitOfWork = new UnitOfWork(context);
            _loginRepository = new LoginRepository(unitOfWork);
        }


        public void UserDeclaration()
        {
            testUserOne = new Users()
            {
                Id=1,
                Username = "menaka",
                Password = "koumiya123",
                Email = "koumiyagowthami232@gmail.com",
                PhoneNumber = 7401222134
            };
            testUserTwo = new Users()
            {
                Id = 2,
                Username = "subramani",
                Password = "koumiya123",
                Email = "koumiyagowthami232@gmail.com",
                PhoneNumber = 7401222134
            };
        }
        public void ObjectAddToDb()
        {
            using (var Context=new assignmentDb(options))
            {
                Context.users.Add(testUserOne);
                Context.users.Add(testUserTwo);
                Context.SaveChanges();
            }
        }
        [Test]
        public void AddUserUnitTesting()
        {
            ObjectAddToDb();
            var result =  _loginRepository.Add(testUserOne);
            Assert.IsNotNull(result);
        }
        [Test]
        public void RemoveUserUnitTesting()
        {
            ObjectAddToDb();
            var result=  _loginRepository.Remove(testUserOne);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Result);
        }
        [Test]
        public void UpdateUserUnitTesting()
        {
            ObjectAddToDb();
            var result =  _loginRepository.Update(testUserOne);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Result);
        }

        [Test]
        public void GetAllUserUnitTesting()
        {
            ObjectAddToDb();
            var result=  _loginRepository.GetAllUser();
            Assert.IsNotNull(result.Result);
            Assert.AreEqual(2, result.Result.Count);
        }
        [Test]
        public void GetUserByNameUnittesting()
        {
            ObjectAddToDb();
            string expectedUsername = "menaka";
            var result =  _loginRepository.GetUserByName(expectedUsername);
            Assert.IsNotNull(result.Result);
            Assert.AreEqual(expectedUsername,result.Result.Username);
        }
        [Test]
        public void GetUserByEmailUnitTesting()
        {
            ObjectAddToDb();
            string Email = "koumiyagowthami232@gmail.com";
            var result =_loginRepository.GetUserByEmail(Email);
            Assert.IsNotNull(result.Result);
            Assert.AreEqual(Email,result.Result.Email);
        }
    }
}