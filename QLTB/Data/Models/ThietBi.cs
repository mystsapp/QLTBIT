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
        [DisplayName("Ten TB")]
        public string TenThietBi { get; set; }

        public double Gia { get; set; }
        public bool TrangThai { get; set; }

        [MaxLength(500), Column(TypeName = "nvarchar(500)")]
        [DisplayName("Dien Giai")]
        public string DienGiai { get; set; }

        public int BaoHanh { get; set; }

        [MaxLength(200), Column(TypeName = "varchar(200)")]
        public string HinhAnh { get; set; }

        [DisplayName("Ten Loai")]
        public int LoaiThietBiId { get; set; }

        [ForeignKey("LoaiThietBiId")]
        public virtual LoaiThietBi LoaiThietBi { get; set; }
    }
}