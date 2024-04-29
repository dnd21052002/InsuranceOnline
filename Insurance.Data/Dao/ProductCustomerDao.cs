using Insurance.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Data.Dao
{
    public class ProductCustomerDao
    {
        private InsuranceDbContext db = null;

        public ProductCustomerDao()
        {
            db = new InsuranceDbContext();
        }

        public List<ProductCustomer> ListAll()
        {
            return db.ProductCustomers.ToList();
        }

        public ProductCustomer ViewDetail(int id)
        {
            return db.ProductCustomers.Find(id);
        }

        public List<ProductCustomer> ListByProduct(int productId)
        {
            return db.ProductCustomers.Where(x => x.ProductID == productId).ToList();
        }

        public List<ProductCustomer> ListByCustomer(int customerId)
        {
            return db.ProductCustomers.Where(x => x.CustomerID == customerId).ToList();
        }

        public bool Insert(ProductCustomer entity)
        {
            try
            {
                db.ProductCustomers.Add(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
