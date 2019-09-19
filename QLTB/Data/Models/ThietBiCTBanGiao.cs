using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLTB.Data.Models
{
    public class ThietBiCTBanGiao
    {
        public int Id { get; set; }

        [DisplayName("Thiet Bi")]
        public int ThietBiId { get; set; }

        [ForeignKey("ThietBiId")]
        public virtual ThietBi ThietBi { get; set; }

        public int ChiTietBanGiaoId { get; set; }

        [ForeignKey("ChiTietBanGiaoId")]
        public virtual ChiTietBanGiao ChiTietBanGiao { get; set; }
    }
}