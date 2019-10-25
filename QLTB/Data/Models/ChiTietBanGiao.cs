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
        [DisplayName("Người Lập")]
        public int BanGiaoId { get; set; }

        [ForeignKey("BanGiaoId")]
        public virtual BanGiao BanGiao { get; set; }



        [DisplayName("Thiết Bị")]
        public int ThietBiId { get; set; }

        [ForeignKey("ThietBiId")]
        public virtual ThietBi ThietBi { get; set; }

        [DisplayName("Số Lượng")]
        public int SoLuong { get; set; }

        [DisplayName("Đơn Giá")]
        public double DonGia { get; set; }

        [DisplayName("Bảo Hành Đến")]
        public DateTime? BaoHanhDen { get; set; }

        [MaxLength(250), Column(TypeName = "nvarchar(250)")]
        [DisplayName("Ghi Chú")]
        public string GhiChu { get; set; }

        [DisplayName("Chuyển Sử Dụng")]
        public bool ChuyenSuDung { get; set; }

        [DisplayName("Sử Dụng")]
        public bool TinhTrang { get; set; }

        [MaxLength(500), Column(TypeName = "nvarchar(500)")]
        [DisplayName("Diễn Giải")]
        public string DienGiai { get; set; }

        [DisplayName("Ngày Tạo")]
        public DateTime? NgayGiao { get; set; }

        [DisplayName("Ngày Chuyển")]
        public DateTime? NgayChuyen { get; set; }
    }
}