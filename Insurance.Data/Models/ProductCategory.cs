﻿using System;
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

        [Required(ErrorMessage = "Tên danh mục không được để trống")]
        [MaxLength(256)]
        public string Name { set; get; }

        public string Alias { set; get; }
        public string Description { set; get; }
        public int? DisplayOrder { set; get; }
        public bool? HomeFlag { set; get; }
        public string Image { set; get; }

        public virtual IEnumerable<Product> Products { set; get; }

        [Required(ErrorMessage = "Phải chọn trạng thái")]
        public bool Status { set; get; }
    }
}