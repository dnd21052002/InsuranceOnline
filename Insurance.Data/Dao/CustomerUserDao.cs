using Insurance.Data.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Data.Dao
{
    public class CustomerUserDao
    {
        private InsuranceDbContext db = null;
      
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

        public List<CustomerUser> ListAll()
        {
            return db.CustomerUsers.OrderBy(x => x.Id).ToList();
        }

        public IEnumerable<CustomerUser> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<CustomerUser> model = db.CustomerUsers;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.FullName.Contains(searchString) || x.Email.Contains(searchString) || x.Username.Contains(searchString));
            }
            return model.OrderByDescending(x => x.Id).ToPagedList(page, pageSize);
        }

        public CustomerUser GetByUsername(string userName)
        {
            return db.CustomerUsers.SingleOrDefault(x => x.Username == userName);
        }

        public CustomerUser ViewDetail(long id)
        {
            return db.CustomerUsers.Find(id);
        }

        public bool Update(CustomerUser entity)
        {
            var user = db.CustomerUsers.Find(entity.Id);
            if (user != null)
            {
                user.FullName = entity.FullName;
                user.Email = entity.Email;
                user.Password = entity.Password;
                user.Address = entity.Address;
                user.Phone = entity.Phone;
                user.BirthDay = entity.BirthDay;
                user.Status = entity.Status;
                if (!string.IsNullOrEmpty(entity.Password))
                {
                    user.Password = entity.Password;
                }
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Delete(long id)
        {
            try
            {
                var customerUser = db.CustomerUsers.Find(id);
                db.CustomerUsers.Remove(customerUser);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

