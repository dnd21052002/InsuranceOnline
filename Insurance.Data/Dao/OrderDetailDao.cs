using Insurance.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Data.Dao
{
    public class OrderDetailDao
    {
        private InsuranceDbContext db = null;

        public OrderDetailDao()
        {
            db = new InsuranceDbContext();
        }

        public bool Insert(OrderDetail entity)
        {
            try
            {
                db.OrderDetails.Add(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<OrderDetail> GetOrderDetailsByOrderId(long orderId)
        {
            var orderDetailsList = db.OrderDetails
                                           .Where(od => od.OrderID == orderId)
                                           .Include(od => od.Product)
                                           .ToList();

            return orderDetailsList;
        }
    }
}