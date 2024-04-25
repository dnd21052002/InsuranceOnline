using Insurance.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Data
{
    public class InsuranceDbContext : DbContext
    {
        public InsuranceDbContext() : base("InsuranceConnection")
        {
            this.Configuration.LazyLoadingEnabled = true;
        }

        public DbSet<AdminUser> AdminUsers { get; set; }
        public DbSet<CustomerUser> CustomerUsers { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Order> Orders { set; get; }
        public DbSet<OrderDetail> OrderDetails { set; get; }
        public DbSet<ProductCustomer> ProductCustomers { set; get; }

        public static InsuranceDbContext Create()
        {
            return new InsuranceDbContext();
        }

    }
}
