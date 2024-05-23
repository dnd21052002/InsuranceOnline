using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InsuranceOnline.Models
{
    public class ClaimViewModel
    {
        [Display(Name = "Họ tên")]
        [Required(ErrorMessage = "Họ tên không được để trống")]
        public string FullName { get; set; }
        [Display(Name = "Số CMND/CCCD")]
        [Required(ErrorMessage = "Số CMND/CCCD không được để trống")]
        public string CitizenId { get; set; }
        [Display(Name = "Loại bảo hiểm")]
        [Required(ErrorMessage = "Loại bảo hiểm không được để trống")]
        public int SelectedInsuranceId { get; set; }
        public List<SelectListItem> Insurances { get; set; }
        [Display(Name = "Tài liệu đính kèm")]
        [Required(ErrorMessage = "Tài liệu đính kèm không được để trống")]
        public IEnumerable<HttpPostedFileBase> Documents { get; set; }
    }
}