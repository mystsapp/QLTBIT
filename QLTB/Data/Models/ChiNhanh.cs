using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLTB.Data.Models
{
    public class ChiNhanh
    {
        public int Id { get; set; }

        [DisplayName("Ma CN")]
        [Required]
        [MaxLength(5), Column(TypeName = "varchar(5)")]
        public string MaChiNhanh { get; set; }

        [Required]
        [DisplayName("Ten CN")]
        [MaxLength(100), Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }

        [MaxLength(250), Column(TypeName = "nvarchar(250)")]
        public string DiaChi { get; set; }

        [MaxLength(20)]
        public string DienThoai { get; set; }
    }
}