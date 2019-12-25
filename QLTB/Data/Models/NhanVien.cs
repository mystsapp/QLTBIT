using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLTB.Data.Models
{
    public class NhanVien
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50), Column(TypeName = "nvarchar(50)")]
        [DisplayName("Họ Tên")]
        public string Name { get; set; }

        [DisplayName("Phòng Ban")]
        [MaxLength(50), Column(TypeName = "nvarchar(50)")]
        public string PhongBan { get; set; }

        [MaxLength(10), Column(TypeName = "varchar(10)")]
        [DisplayName("Khối")]
        public string Khoi { get; set; }

        //[DisplayName("Chi Nhánh")]
        //public int ChiNhanhId { get; set; }

        //[ForeignKey("ChiNhanhId")]
        //public virtual ChiNhanh ChiNhanh { get; set; }

        [DisplayName("Văn Phòng")]
        public int VanPhongId { get; set; }

        [ForeignKey("VanPhongId")]
        public virtual VanPhong VanPhong { get; set; }

        //[DisplayName("Văn Phòng")]
        //[MaxLength(50), Column(TypeName = "nvarchar(50)")]
        //public string VanPhong { get; set; }

        [DisplayName("Tình Trạng")]
        public bool TinhTrang { get; set; }

    }
}