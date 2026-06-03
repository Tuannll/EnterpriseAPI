namespace NguyenVietTuanAnh0204068De1.Entities
{
    public class DoanhNghiep0204068De1
    {
        public int Id { get; set; }
        public string TenDoanhNghiep { get; set; } = string.Empty;
        public string MaSoThue { get; set; } = string.Empty;
        public string? DiaChi { get; set; }
        public ICollection<DoanhNghiepSanPham0204068De1> DoanhNghiepSanPhams { get; set; }
            = new List<DoanhNghiepSanPham0204068De1>();
    }
}