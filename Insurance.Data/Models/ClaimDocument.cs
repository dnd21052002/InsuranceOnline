using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Data.Models
{
    [Table("ClaimDocuments")]
    public class ClaimDocument
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public int ClaimId { get; set; }
        public virtual Claim Claim { get; set; }
    }
}
