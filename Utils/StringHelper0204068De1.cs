namespace NguyenVietTuanAnh0204068De1.Utils
{
    public static class StringHelper0204068De1
    {
        public static string? TrimOrNull(string? value)
        {
            if (value == null) return null;
            var trimmed = value.Trim();
            return string.IsNullOrEmpty(trimmed) ? null : trimmed;
        }

        public static string TrimOrEmpty(string? value)
        {
            return value?.Trim() ?? string.Empty;
        }

        public static string? NormalizeKeyword(string? keyword)
        {
            var trimmed = keyword?.Trim();
            return string.IsNullOrEmpty(trimmed) ? null : trimmed.ToLowerInvariant();
        }
    }
}