using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLTB.Data.Models
{
    public class NhapKho
    {
        public int Id { get; set; }

        [DisplayName("Tên Thiết Bị")]
        [Column(TypeName = "nvarchar(100)")]
        [Required]
        public string TenThietBi { get; set; }

        [DisplayName("Diễn Giải")]
        [Column(TypeName = "nvarchar(500)")]
        public string DienGiaiTB { get; set; }

        [DisplayName("Người Nhập Kho")]
        [Column(TypeName = "nvarchar(50)")]
        public string NguoiNhapKho { get; set; }

        [DisplayName("Ngày Sử Dụng")]
        public DateTime? NgaySuDung { get; set; }

        [DisplayName("Người Sử Dụng")]
        [Column(TypeName = "nvarchar(50)")]
        public string NguoiSuDung { get; set; }

        [DisplayName("Lý Do")]
        [Column(TypeName = "nvarchar(500)")]
        public string LyDo { get; set; }

        [DisplayName("Ngày Nhập Kho")]
        public DateTime? NgayNhapKho { get; set; }

        [DisplayName("Kho Văn Phòng")]
        [Column(TypeName = "nvarchar(100)")]
        public string KhoVanPhong { get; set; }

        [DisplayName("Thanh Lý")]
        public bool ThanhLy { get; set; }

        [DisplayName("Ngày Thanh Lý")]
        public DateTime? NgayThanhLy { get; set; }

        [DisplayName("Người Thanh Lý")]
        [Column(TypeName = "nvarchar(50)")]
        public string NguoiThanhLy { get; set; }

        [DisplayName("CTBG Id")]
        public int CTBGId { get; set; }

        
        [DisplayName("Khu Vực")]
        [Column(TypeName = "nvarchar(10)")]
        public string KhuVuc { get; set; }
    }
}