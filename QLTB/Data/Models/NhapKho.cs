using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

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
        //[DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
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
        [ForeignKey("CTBGId")]
        public virtual ChiTietBanGiao ChiTietBanGiao { get; set; }
    }
}
