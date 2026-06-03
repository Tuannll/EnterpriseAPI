using System.ComponentModel.DataAnnotations;

namespace NguyenVietTuanAnh0204068De1.Dtos.DoanhNghiep
{
    public class LocDoanhNghiepDto0204068De1
    {
        private string? _keyword;

        [Range(1, int.MaxValue, ErrorMessage = "PageIndex phai lon hon hoac bang 1.")]
        public int PageIndex { get; set; } = 1;

        [Range(1, 100, ErrorMessage = "PageSize phai tu 1 den 100.")]
        public int PageSize { get; set; } = 10;

        public string? Keyword
        {
            get => _keyword;
            set => _keyword = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
        }
    }
}