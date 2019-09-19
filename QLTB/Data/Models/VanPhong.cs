using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLTB.Data.Models
{
    public class VanPhong
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100), Column(TypeName = "nvarchar(100)")]
        [DisplayName("Van Phong")]
        public string TenVP { get; set; }

        [MaxLength(250), Column(TypeName = "nvarchar(250)")]
        public string DiaChi { get; set; }

        [MaxLength(15)]
        public string DienThoai { get; set; }

        [DisplayName("Chi Nhanh")]
        public int ChiNhanhId { get; set; }

        [ForeignKey("ChiNhanhId")]
        public virtual ChiNhanh ChiNhanh { get; set; }
    }
}