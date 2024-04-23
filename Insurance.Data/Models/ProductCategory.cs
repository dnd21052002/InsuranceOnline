using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Data.Models
{
    [Table("ProductCategories")]
    public class ProductCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [Required]
        [MaxLength(256)]
        public string Name { set; get; }

        [Required]
        [MaxLength(256)]
        public string Alias { set; get; }
        public string Description { set; get; }
        public int? DisplayOrder { set; get; }
        public bool? HomeFlag { set; get; }

        public virtual IEnumerable<Product> Products { set; get; }
        public int Status { set; get; }
    }
}
