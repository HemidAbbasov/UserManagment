using System;
using System.Linq;

using LinqToDB;

using NUnit.Framework;

using UserManagmentLib.DataModels;

namespace UserManagmentTests
{
    [TestFixture]
    public class TestRoles : TestBase
    {
        [Test]
        public void SelectAllRoles()
        {
            using (var db = new FirstDB())
            {
                var allRoles = from r in db.Roles
                               select r;
                GetResult(allRoles);
            }
        }

        [Test]
        public void SelectUsersByRole()
        {
            using (var db = new FirstDB())
            {
                var users = from r in db.Roles
                            from u in r.Users
                            where r.Id == 1
                            select u;
                GetResult(users);
            }
        }

        [Test]
        public void InsertNewRole()
        {
            using ( var db = new FirstDB() )
            {
                db.Roles.Insert(() => new Role {Name = "TEST"});
            }
        }

        [Test]
        public void AnyRole()
        {
            using ( var db = new FirstDB() )
            {
                var exists = db.Roles.Any(r => r.Name == "ADMN");
                Console.WriteLine(exists);
            }
        }
    }
}