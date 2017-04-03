using System;

using LinqToDB;

using NUnit.Framework;

using UserManagmentLib.DataModels;

namespace UserManagmentTests
{
    [TestFixture]
    public class TestUsers : TestBase
    {
        [Test]
        public void InsertUser()
        {
            using ( var db = new FirstDB() )
            {
                db.Users.Insert(() =>
                                new User
                                {
                                    UserName = "Test",
                                    Surname = "Test",
                                    Firstname = "Test",
                                    Patronymic = "Test",
                                    BirthDate = new DateTime(1980, 12, 24),
                                    Email = "test@test.com",
                                    Sex = "M",
                                    MobileNumber = "111111",
                                    RoleId = 1
                                });
            }
        }
    }
}