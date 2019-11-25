using QLTB.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QLTB.Models
{
    public class EditUserViewModel
    {
        public EditUserViewModel()
        {
            Claims = new List<string>();
            Roles = new List<string>();
        }
        public string Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required][EmailAddress]
        public string Email { get; set; }

        public List<string> Claims { get; set; }
        public IList<string> Roles { get; set; }
        [Required]
        [DisplayName("Họ Tên")]
        public string Name { get; set; }

        [DisplayName("Phòng Ban")]
        public string PhongBan { get; set; }

        [DisplayName("Khối")]
        public string Khoi { get; set; }

        [DisplayName("Chi Nhánh")]
        public int ChiNhanhId { get; set; }

        [DisplayName("Văn Phòng")]
        public string VanPhong { get; set; }

        [DisplayName("Tình Trạng")]
        public bool TinhTrang { get; set; }

        public IEnumerable<ChiNhanh> ChiNhanhs { get; set; }
        public IEnumerable<VanPhong> VanPhongs { get; set; }

        [Display(Name = "Phone")]
        public string PhoneNumber { get; set; }
    }
}
