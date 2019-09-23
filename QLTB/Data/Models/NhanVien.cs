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
        [DisplayName("Ho Ten")]
        public string Name { get; set; }

        [DisplayName("Phong Ban")]
        [MaxLength(50), Column(TypeName = "nvarchar(50)")]
        public string PhongBan { get; set; }

        [MaxLength(10), Column(TypeName = "varchar(10)")]
        [DisplayName("Khoi")]
        public string Khoi { get; set; }

        [DisplayName("Chi Nhanh")]
        public int ChiNhanhId { get; set; }

        [ForeignKey("ChiNhanhId")]
        public virtual ChiNhanh ChiNhanh { get; set; }

        [DisplayName("Van Phong")]
        [MaxLength(50), Column(TypeName = "nvarchar(50)")]
        public string VanPhong { get; set; }
    }
}