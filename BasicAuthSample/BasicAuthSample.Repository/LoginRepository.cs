using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasicAuthSample.Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace BasicAuthSample.Repository
{
    public class LoginRepository
    {
        public static Login GetUserByUserName(string userName)
        {
            HospitalMgmtContext database = new HospitalMgmtContext();
            return database.Logins
                .Include(l => l.UserRoles)
                .ThenInclude(ur => ur.Role)
                .Where(u => u.UserName == userName).FirstOrDefault();
        }
        public static List<Login> GetUsers(UserTypeEnum type)
        {
            HospitalMgmtContext database = new HospitalMgmtContext();
            return database.Logins.Where(u => u.UserTypeId == (int)type).ToList();
        }
        public static Login Register(Login login)
        {
            HospitalMgmtContext database = new HospitalMgmtContext();
            database.Logins.Add(login);
            database.SaveChanges();
            return login;
        }

        public static Login IsLoginValid(Login login)
        {
            HospitalMgmtContext database = new HospitalMgmtContext();
            Login dbRecord = database.Logins
                .Include(l => l.UserRoles)
                    .ThenInclude(ur => ur.Role)
                    .Where(loginRecord => (loginRecord.UserName == login.UserName)
                                    && (loginRecord.Password == login.Password)).FirstOrDefault();
            if (dbRecord != null)
            {
                return dbRecord;
            }
            else
            {
                return null;
            }
        }
    }
}
