using Insurance.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using PagedList;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Insurance.Data.Dao
{
    public class ProductCategoryDao
    {
        InsuranceDbContext db = null;
        public ProductCategoryDao()
        {
            db = new InsuranceDbContext();
        }

        public List<ProductCategory> ListAll()
        {
            return db.ProductCategories.Where(x => x.Status).OrderBy(x => x.DisplayOrder).ToList();
        }

        public IEnumerable<ProductCategory> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<ProductCategory> model = db.ProductCategories;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString) || x.Description.Contains(searchString));
            }
            return model.OrderByDescending(x => x.DisplayOrder).ToPagedList(page, pageSize);
        }

        public ProductCategory ViewDetail(long id)
        {
            return db.ProductCategories.Find(id);
        }

        public long Insert(ProductCategory entity)
        {
            if (string.IsNullOrEmpty(entity.Alias))
            {
                entity.Alias = StringHelper.ToUnsignString(entity.Name);
            }
            db.ProductCategories.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        public bool Update(ProductCategory entity)
        {
            if (string.IsNullOrEmpty(entity.Alias))
            {
                entity.Alias = StringHelper.ToUnsignString(entity.Name);
            }
            //luu lai du lieu cu
            var oldProductCate = db.ProductCategories.Find(entity.ID);
            db.ProductCategories.AddOrUpdate(entity);
            db.SaveChanges();
            return true;
        }

        public bool Delete(long id)
        {
            try
            {
                var productCategory = db.ProductCategories.Find(id);
                db.ProductCategories.Remove(productCategory);
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
