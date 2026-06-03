using System;
using System.Collections.Generic;

namespace NguyenVietTuanAnh0204068De1.Entities
{
    public class SanPham0204068De1
    {
        public int Id { get; set; }
        public string TenSanPham { get; set; } = string.Empty;
        public string MaSanPham { get; set; } = string.Empty;
        public DateTime NgayNhap { get; set; }
        public ICollection<DoanhNghiepSanPham0204068De1> DoanhNghiepSanPhams { get; set; }
            = new List<DoanhNghiepSanPham0204068De1>();
    }
}