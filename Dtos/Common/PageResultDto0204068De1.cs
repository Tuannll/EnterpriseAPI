using System.Collections.Generic;

namespace NguyenVietTuanAnh0204068De1.Dtos.Common
{
    public class PageResultDto0204068De1<T>
    {
        public int TotalCount { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public List<T> Items { get; set; } = new List<T>();
        public int TotalPages => PageSize > 0
            ? (int)System.Math.Ceiling((double)TotalCount / PageSize)
            : 0;
    }
}