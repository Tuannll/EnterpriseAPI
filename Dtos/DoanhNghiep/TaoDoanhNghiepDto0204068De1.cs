using System.ComponentModel.DataAnnotations;

namespace NguyenVietTuanAnh0204068De1.Dtos.DoanhNghiep
{
    public class TaoDoanhNghiepDto0204068De1
    {
        private string _tenDoanhNghiep = string.Empty;
        private string _maSoThue = string.Empty;
        private string? _diaChi;

        [Required(ErrorMessage = "Ten doanh nghiep khong duoc de trong.")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Ten doanh nghiep phai tu 1 den 200 ky tu.")]
        public string TenDoanhNghiep
        {
            get => _tenDoanhNghiep;
            set => _tenDoanhNghiep = value?.Trim() ?? string.Empty;
        }

        [Required(ErrorMessage = "Ma so thue khong duoc de trong.")]
        [StringLength(20, MinimumLength = 10, ErrorMessage = "Ma so thue phai tu 10 den 20 ky tu.")]
        public string MaSoThue
        {
            get => _maSoThue;
            set => _maSoThue = value?.Trim() ?? string.Empty;
        }

        [StringLength(500, ErrorMessage = "Dia chi khong duoc vuot qua 500 ky tu.")]
        public string? DiaChi
        {
            get => _diaChi;
            set => _diaChi = value?.Trim();
        }
    }
}