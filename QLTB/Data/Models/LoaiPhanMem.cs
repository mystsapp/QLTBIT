using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLTB.Data.Models
{
    public class LoaiPhanMem
    {
        public int Id { get; set; }

        [DisplayName("Loai PM")]
        [Required]
        [MaxLength(100), Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }

        [DisplayName("Ghi Chu")]
        [MaxLength(500), Column(TypeName = "nvarchar(500)")]
        public string GhiChu { get; set; }
    }
}