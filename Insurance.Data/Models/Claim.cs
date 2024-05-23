using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Insurance.Data.Models
{
    [Table("Claims")]
    public class Claim
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FullName { get; set; }
        public string CitizenId { get; set; }
        public int InsuranceId { get; set; }
        public string InsuranceName { get; set; }
        public bool Status { get; set; }
        public virtual ICollection<ClaimDocument> ClaimDocuments { get; set; }

    }
}
