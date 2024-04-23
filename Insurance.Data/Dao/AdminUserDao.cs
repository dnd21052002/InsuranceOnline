using Insurance.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Data.Dao
{
    public class AdminUserDao
    {
        InsuranceDbContext db = null;
        public AdminUserDao()
        {
            db = new InsuranceDbContext();
        }

        public int Insert(AdminUser entity)
        {
            db.AdminUsers.Add(entity);
            db.SaveChanges();
            return entity.Id;
        }

        public int Login(string userName, string password)
        {
            var result = db.AdminUsers.SingleOrDefault(x => x.Username == userName);
            if (result == null)
            {
                return 0;
            }
            else
            {
                if (result.Status == -1)
                {
                    return -1;
                }
                else
                {
                    if (result.Password == password)
                    {
                        if(result.Status == 1)
                        {
                            return 1;
                        }
                        else
                        {
                            return -1;
                        }
                        
                    }
                    else
                    {
                        return -2;
                    }
                }
            }
        }

        public AdminUser GetByID(string userName)
        {
            return db.AdminUsers.SingleOrDefault(x => x.Username == userName);
        }
    }
}
