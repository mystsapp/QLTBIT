using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLTB.Data.Models
{
    public class ThietBi
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100), Column(TypeName = "nvarchar(100)")]
        [DisplayName("Tên TB")]
        public string Name { get; set; }

        [DisplayName("Giá")]
        public double Gia { get; set; }

        [DisplayName("Trạng Thái")]
        public bool TrangThai { get; set; }

        [MaxLength(500), Column(TypeName = "nvarchar(500)")]
        [DisplayName("Diễn Giải")]
        public string DienGiai { get; set; }

        [DisplayName("Bảo Hành")]
        public int BaoHanh { get; set; }

        [DisplayName("Hình Ảnh")]
        [MaxLength(200), Column(TypeName = "varchar(200)")]
        public string HinhAnh { get; set; }

        [DisplayName("Tên Loại TB")]
        public int LoaiThietBiId { get; set; }

        [ForeignKey("LoaiThietBiId")]
        public virtual LoaiThietBi LoaiThietBi { get; set; }
    }
}