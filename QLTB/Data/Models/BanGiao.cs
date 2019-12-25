using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLTB.Data.Models
{
    public class BanGiao
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Người Lập")]
        [MaxLength(50), Column(TypeName = "nvarchar(50)")]
        public string NguoiLap { get; set; }

        [DisplayName("Người Nhận")]
        [Required]
        [MaxLength(50), Column(TypeName = "nvarchar(50)")]
        public string NguoiNhan { get; set; }

        public DateTime NgayTao { get; set; }

        
        public string NguoiSua { get; set; }
        public DateTime? NgaySua { get; set; }

        //[Required]
        //[DisplayName("Chi Nhánh")]
        //public int ChiNhanhId { get; set; }

        //[ForeignKey("ChiNhanhId")]
        //public virtual ChiNhanh ChiNhanh { get; set; }

        [Required]
        [DisplayName("Văn Phòng")]
        public int VanPhongId { get; set; }

        [ForeignKey("VanPhongId")]
        public virtual VanPhong VanPhong { get; set; }

        //[DisplayName("Văn Phòng")]
        //[MaxLength(50), Column(TypeName = "nvarchar(50)")]
        //[Required]
        //public string VanPhong { get; set; }

        [DisplayName("Khối")]
        [MaxLength(50), Column(TypeName = "varchar(50)")]
        public string Khoi { get; set; }

        [DisplayName("Phòng Ban")]
        [MaxLength(50), Column(TypeName = "nvarchar(50)")]
        public string PhongBan { get; set; }

        public bool TinhTrang { get; set; }

        [DisplayName("Lý Do")]
        [MaxLength(250), Column(TypeName = "nvarchar(50)")]
        public string LyDo { get; set; }

        [DisplayName("Thiết Bị")]
        [Column(TypeName = "nvarchar(50)")]
        public string LoaiThietBi { get; set; }


        //public string KhuVuc { get; set; }
    }
}