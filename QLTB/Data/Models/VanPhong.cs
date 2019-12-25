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
        [DisplayName("Văn Phòng")]
        public string Name { get; set; }

        [MaxLength(250), Column(TypeName = "nvarchar(250)")]
        [DisplayName("Ghi Chú")]
        public string DiaChi { get; set; }

        [MaxLength(15)]
        [DisplayName("Điện Thoại")]
        public string DienThoai { get; set; }

        [DisplayName("Chi Nhánh")]
        public int ChiNhanhId { get; set; }

        [ForeignKey("ChiNhanhId")]
        public virtual ChiNhanh ChiNhanh { get; set; }


        [DisplayName("Khu Vực")]
        [MaxLength(20), Column(TypeName = "nvarchar(20)")]
        public string KhuVuc { get; set; }
    }
}