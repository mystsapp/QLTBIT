using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLTB.Data.Models
{
    public class ChiTietBanGiao
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Nguoi Lap")]
        public int BanGiaoId { get; set; }

        [ForeignKey("BanGiaoId")]
        public virtual BanGiao BanGiao { get; set; }

        [DisplayName("Thiet Bi")]
        [Required]
        public int ThietBiId { get; set; }

        [ForeignKey("ThietBiId")]
        public virtual ThietBi ThietBi { get; set; }

        public int SoLuong { get; set; }
        public double DonGia { get; set; }

        [DisplayName("Bao Hanh Den")]
        public DateTime BaoHanhDen { get; set; }

        [MaxLength(250), Column(TypeName = "nvarchar(250)")]
        public string GhiChu { get; set; }

        [DisplayName("Tinh Trang")]
        public bool TinhTrang { get; set; }
        [MaxLength(500), Column(TypeName = "nvarchar(500)")]
        public string DienGiai { get; set; }

        public DateTime NgayGiao { get; set; }

    }
}