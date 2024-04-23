using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Data.Models
{
    public class Cart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int CustomerUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Status { get; set; }
        public bool IsDeleted { get; set; }
        [ForeignKey("CustomerUserId")]
        public virtual CustomerUser CustomerUser { get; set; }
        public virtual IEnumerable<CartItem> CartItems { get; set; }
    }
}
