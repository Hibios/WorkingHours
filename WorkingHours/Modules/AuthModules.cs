using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static WorkingHours.EF.Connection;
using WorkingHours.Model;

namespace WorkingHours.Modules
{
    public class AuthModules
    {
        

        public static void AddNewUser(string password, string name, string email, System.DateTime birthday)
        {
            string[] fio = name.Split(' ');


            User user = new User
            {
                IdUser = db.Users.ToList().Count() + 1,
                Email = email,
                Password = password,
                FirstName = fio[1],
                MiddleName = fio[0],
                Patronymic = fio[2],
                DateBirthday = birthday,
                isVaitingForOrg = false
            };
            db.Users.Add(user);
            db.SaveChanges();
        }

        public static void UpdateUser(string password, string name, string email, System.DateTime birthday)
        {
            string[] fio = name.Split(' ');


            UserForRole.userForRole.Email = email;
            UserForRole.userForRole.Password = password;
            UserForRole.userForRole.FirstName = fio[1];
            UserForRole.userForRole.MiddleName = fio[0];
            UserForRole.userForRole.Patronymic = fio[2];
            UserForRole.userForRole.DateBirthday = birthday;
            db.SaveChanges();
        }

        public static bool IsValidEmail(string email)
        {
            try
            {
                var mail = new System.Net.Mail.MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
        }

        internal static void CreateOrganization(User userForRole, string orgName, OrganizationType selectedValue)
        {
            Organization organization = new Organization
            {
                IdOrganization = db.Organizations.ToList().Count() + 1,
                Name = orgName,
                IdType = selectedValue.IdType
            };
            db.Organizations.Add(organization);
            userForRole.IdOrganization = organization.IdOrganization;
            userForRole.isOrganizationOwner = true;
            userForRole.Role = db.Roles.Where(r => r.RoleName == "Генеральный директор").FirstOrDefault();
            db.SaveChanges();
        }
    }
}
