using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Data.Models
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [Required(ErrorMessage = "vui lòng không để trống")]
        [MaxLength(256)]
        public string CustomerName { set; get; }

        [Required(ErrorMessage = "vui lòng không để trống")]
        [MaxLength(256)]
        public string CustomerAddress { set; get; }

        [Required(ErrorMessage = "vui lòng không để trống")]
        [MaxLength(256)]
        public string CustomerEmail { set; get; }

        //CMND
        [Required(ErrorMessage = "vui lòng không để trống")]
        [MaxLength(15)]
        public string CustomerIdentity { set; get; }

        [Required(ErrorMessage = "vui lòng không để trống")]
        [MaxLength(20)]
        public string CustomerMobile { set; get; }

        public string CustomerMessage { set; get; }

        public DateTime? CreatedDate { set; get; }

        public string CreatedBy { set; get; }

        public decimal TotalPrice { set; get; }

        public string PaymentMethod { set; get; }

        public bool Status { set; get; }
        public int CustomerID { set; get; }

        [ForeignKey("CustomerID")]
        public virtual CustomerUser User { set; get; }

        public virtual IEnumerable<OrderDetail> OrderDetails { set; get; }
    }
}