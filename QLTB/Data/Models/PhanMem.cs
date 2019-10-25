using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLTB.Data.Models
{
    public class PhanMem
    {
        public int Id { get; set; }

        [MaxLength(50)]
        [DisplayName("Tên PM"), Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }

        [DisplayName("Loại PM")]
        public int LoaiPMId { get; set; }

        [ForeignKey("LoaiPMId")]
        public virtual LoaiPhanMem LoaiPhanMem { get; set; }
    }
}