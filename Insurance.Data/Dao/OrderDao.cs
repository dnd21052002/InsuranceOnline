using Insurance.Data.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Entity;
using System.Runtime.Remoting.Contexts;

namespace Insurance.Data.Dao
{
    public class OrderDao
    {
        private InsuranceDbContext db = null;

        public OrderDao()
        {
            db = new InsuranceDbContext();
        }

        public int Insert(Order entity)
        {
            db.Orders.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        public IEnumerable<Order> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Order> model = db.Orders;

            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.CustomerName.Contains(searchString) || x.CustomerEmail.Contains(searchString) || x.CustomerMobile.Contains(searchString));
            }
            return model.OrderByDescending(x => x.ID).ToPagedList(page, pageSize);
        }

        public Order ViewDetail(long id)
        {
            return db.Orders.Find(id);
        }

        public bool Update(Order entity)
        {
            var order = db.Orders.Find(entity.ID);
            if (order != null)
            {
                order.CustomerName = entity.CustomerName;
                order.CustomerAddress = entity.CustomerAddress;
                order.CustomerEmail = entity.CustomerEmail;
                order.CustomerIdentity = entity.CustomerIdentity;
                order.CustomerMobile = entity.CustomerMobile;
                order.CustomerMessage = entity.CustomerMessage;
                order.CreatedDate = entity.CreatedDate;
                order.Status = entity.Status;
                order.TotalPrice = entity.TotalPrice;
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
                var order = db.Orders.Find(id);
                db.Orders.Remove(order);
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