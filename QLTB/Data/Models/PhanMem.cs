﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLTB.Data.Models
{
    public class PhanMem
    {
        public int Id { get; set; }

        [MaxLength(50)]
        [DisplayName("Ten PM"), Column(TypeName = "nvarchar(50)")]
        public string TenPM { get; set; }

        [DisplayName("Loai PM")]
        public int LoaiPMId { get; set; }

        [ForeignKey("LoaiPMId")]
        public virtual LoaiPhanMem LoaiPhanMem { get; set; }
    }
}