namespace NguyenVietTuanAnh0204068De1.Entities
{
    public class DoanhNghiepSanPham0204068De1
    {
        public int DoanhNghiepId { get; set; }
        public int SanPhamId { get; set; }
        public int SoLuong { get; set; }

        public DoanhNghiep0204068De1 DoanhNghiep { get; set; } = null!;
        public SanPham0204068De1 SanPham { get; set; } = null!;
    }
}