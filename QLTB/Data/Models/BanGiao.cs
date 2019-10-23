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
        [DisplayName("Nguoi Lap")]
        [MaxLength(50), Column(TypeName = "nvarchar(50)")]
        public string NguoiLap { get; set; }

        [DisplayName("Nguoi Nhan")]
        [Required]
        [MaxLength(50), Column(TypeName = "nvarchar(50)")]
        public string NguoiNhan { get; set; }

        public DateTime NgayTao { get; set; }
        public string NguoiSua { get; set; }
        public DateTime? NgaySua { get; set; }

        [Required]
        [DisplayName("Chi Nhanh")]
        public int ChiNhanhId { get; set; }

        [ForeignKey("ChiNhanhId")]
        public virtual ChiNhanh ChiNhanh { get; set; }

        [DisplayName("Van Phong")]
        [MaxLength(50), Column(TypeName = "varchar(50)")]
        [Required]
        public string VanPhong { get; set; }

        [DisplayName("Khoi")]
        [MaxLength(50), Column(TypeName = "varchar(50)")]
        public string Khoi { get; set; }

        [DisplayName("Phong Ban")]
        [MaxLength(50), Column(TypeName = "nvarchar(50)")]
        public string PhongBan { get; set; }

        public bool TinhTrang { get; set; }

        [DisplayName("Ly Do")]
        [MaxLength(250), Column(TypeName = "nvarchar(50)")]
        public string LyDo { get; set; }

        [DisplayName("Thiet Bi")]
        [Column(TypeName = "nvarchar(50)")]
        public string LoaiThietBi { get; set; }

    }
}