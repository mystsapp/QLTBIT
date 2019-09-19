using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLTB.Data.Models
{
    public class LoaiThietBi
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Trường này không được để trống")]
        [MaxLength(100), Column(TypeName = "nvarchar(100)")]
        [DisplayName("Tên Loại")]
        public string Name { get; set; }

        [MaxLength(500), Column(TypeName = "nvarchar(500)")]
        [DisplayName("Ghi Chú")]
        public string GhiChu { get; set; }
    }
}