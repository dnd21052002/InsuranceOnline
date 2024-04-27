using Insurance.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Data.Dao
{
    public class CustomerUserDao
    {
        InsuranceDbContext db = null;
        public CustomerUserDao()
        {
            db = new InsuranceDbContext();
        }

        public int Insert(CustomerUser entity)
        {
            db.CustomerUsers.Add(entity);
            db.SaveChanges();
            return entity.Id;
        }

        public int Login(string userName, string password)
        {
            var result = db.CustomerUsers.SingleOrDefault(x => x.Username == userName);
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
                        if (result.Status == 1)
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

        public CustomerUser GetByUsername(string userName)
        {
            return db.CustomerUsers.SingleOrDefault(x => x.Username == userName);
        }

        public bool CheckUserName(string userName)
        {
            return db.CustomerUsers.Count(x => x.Username == userName) > 0;
        }

        public bool CheckEmail(string email)
        {
            return db.CustomerUsers.Count(x => x.Email == email) > 0;
        }
    }
}
