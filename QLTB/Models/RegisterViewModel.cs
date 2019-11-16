using Microsoft.AspNetCore.Mvc;
using QLTB.Data.Models;
using QLTB.Utility;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace QLTB.Models
{
    public class RegisterViewModel
    {
        [Required]
        [Remote(action: "IsUsernameInUse", controller: "Account")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        //[ValidEmailDomain(allowedDomain: "saigontourist.net", ErrorMessage = "Email domain must be saigontourist.net")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [DisplayName("Confirm Password")]
        [Compare("Password", ErrorMessage = "Password and confirm password do not match.")]
        public string ConformPassword { get; set; }

        [Display(Name = "Phone")]
        public string PhoneNumber { get; set; }

        [Required]
        [MaxLength(50)]
        [DisplayName("Họ Tên")]
        public string Name { get; set; }

        [DisplayName("Phòng Ban")]
        [MaxLength(50)]
        public string PhongBan { get; set; }

        [MaxLength(10)]
        [DisplayName("Khối")]
        public string Khoi { get; set; }

        [DisplayName("Chi Nhánh")]
        public int ChiNhanhId { get; set; }

        [DisplayName("Văn Phòng")]
        [MaxLength(50)]
        public string VanPhong { get; set; }

        [DisplayName("Tình Trạng")]
        public bool TinhTrang { get; set; }
        public IEnumerable<ChiNhanh> ChiNhanhs { get; set; }
    }
}