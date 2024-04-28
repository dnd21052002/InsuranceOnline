using Common;
using Insurance.Data.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Data.Dao
{
    public class AdminUserDao
    {
        private InsuranceDbContext db = null;

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

        public List<AdminUser> ListAll()
        {
            return db.AdminUsers.OrderBy(x => x.Id).ToList();
        }

        public IEnumerable<AdminUser> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<AdminUser> model = db.AdminUsers;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.FullName.Contains(searchString) || x.Email.Contains(searchString));
            }
            return model.OrderByDescending(x => x.Id).ToPagedList(page, pageSize);
        }

        public AdminUser GetByID(string userName)
        {
            return db.AdminUsers.SingleOrDefault(x => x.Username == userName);
        }

        public AdminUser ViewDetail(long id)
        {
            return db.AdminUsers.Find(id);
        }

        public bool Update(AdminUser entity)
        {
            var user = db.AdminUsers.Find(entity.Id);
            if (user != null)
            {
                user.FullName = entity.FullName;
                user.Email = entity.Email;
                user.Status = entity.Status;
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
                var adminUser = db.AdminUsers.Find(id);
                db.AdminUsers.Remove(adminUser);
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