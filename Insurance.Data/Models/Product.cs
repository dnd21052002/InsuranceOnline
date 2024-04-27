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
    [Table("Products")]
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [Required]
        [MaxLength(100)]
        public string Name { set; get; }

        [Required]
        public int CategoryID { set; get; }

        [Required]
        public decimal Price { set; get; }

        public decimal? PromotionPrice { set; get; }

        public int? Warranty { set; get; }

        [Required]
        public string Description { set; get; }

        public bool? HomeFlag { set; get; }
        public bool? HotFlag { set; get; }

        //0 là tháng, 1 là năm
        public int ExpireType { set; get; }

        //thời gian có hiệu lực theo tháng hoặc năm
        public int ExpireTime { set; get; }

        [ForeignKey("CategoryID")]
        public virtual ProductCategory ProductCategory { set; get; }

        public virtual IEnumerable<OrderDetail> OrderDetails { set; get; }
        public bool Status { set; get; }
    }
}
