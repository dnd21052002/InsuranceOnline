using Insurance.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Data.Dao
{
    public class CartItemDao
    {
        private InsuranceDbContext db = null;

        public CartItemDao()
        {
            db = new InsuranceDbContext();
        }

        public void Insert(CartItem entity)
        {
            db.CartItems.Add(entity);
            db.SaveChanges();
        }

        public void Update(CartItem entity)
        {
            var cartItem = db.CartItems.Find(entity.ID);
            if (cartItem != null)
            {
                cartItem.Quantity = entity.Quantity;
                db.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var cartItem = db.CartItems.Find(id);
            if (cartItem != null)
            {
                db.CartItems.Remove(cartItem);
                db.SaveChanges();
            }
        }

        public List<CartItem> GetByCart(long cartId)
        {
            return db.CartItems.Where(x => x.CartID == cartId).ToList();
        }

        public CartItem GetByCartAndProduct(long cartId, long productId)
        {
            return db.CartItems.SingleOrDefault(x => x.CartID == cartId && x.ProductID == productId);
        }
    }
}
