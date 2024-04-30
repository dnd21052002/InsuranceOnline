using Insurance.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Data.Dao
{
    public class CartDao
    {
        private InsuranceDbContext db = null;

        public CartDao()
        {
            db = new InsuranceDbContext();
        }

        public long Insert(Cart cart)
        {
            db.Carts.Add(cart);
            db.SaveChanges();
            return cart.Id;
        }

        public List<CartItem> GetByCart(long cartId)
        {
            return db.CartItems.Where(x => x.CartID == cartId).ToList();
        }

        public Cart GetByUser(long userId)
        {
            return db.Carts.SingleOrDefault(x => x.CustomerUserId == userId && x.IsDeleted == false);
        }

        public Cart Delete(long id)
        {
            var cart = db.Carts.Find(id);
            cart.IsDeleted = true;
            cart.Status = false;
            db.SaveChanges();
            return cart;
        }
    }
}
