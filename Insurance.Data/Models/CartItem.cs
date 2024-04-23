using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Data.Models
{
    [Table("CartItems")]
    public class CartItem
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ID { set; get; }
        public int CartID { set; get; }
        public int ProductID { set; get; }
        public int Quantity { set; get; }
        [ForeignKey("CartID")]
        public virtual Cart Cart { set; get; }
        [ForeignKey("ProductID")]
        public virtual Product Product { set; get; }
    }
}
