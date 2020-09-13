using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agriculture.DomainClass.Models;

namespace Agriculture.Data
{
    public class AgricultureInitializer : System.Data.Entity.CreateDatabaseIfNotExists<AgricultureContext>
    {
        protected override void Seed(AgricultureContext context)
        {
            var roles = new List<Role>
            {
                new Role{RoleId= 1, RoleName = "مدیر"},
                new Role{RoleId= 2, RoleName = "کاربر عادی"},
            };

            roles.ForEach(s => context.Roles.Add(s));
            context.SaveChanges();
            var humans = new List<Human>
            {
                new Human{HumanId= 1, FirstName = "مدیر",LastName = "admin",NationalCode = "1111111111", MobileNumber = "09160271868"},
            };

            humans.ForEach(s => context.Humans.Add(s));
            context.SaveChanges();

            var user = new List<User>
            {
                new User{UserId= 1, UserName = "admin",Password = "1", RoleId = 1,HumanId = 1,RegisterDate = DateTime.Parse("2020-09-03")},
            };

            user.ForEach(s => context.Users.Add(s));
            context.SaveChanges();

        }
    }
}
