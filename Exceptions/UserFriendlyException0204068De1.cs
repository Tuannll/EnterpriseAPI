using System;

namespace NguyenVietTuanAnh0204068De1.Exceptions
{
    public class UserFriendlyException0204068De1 : Exception
    {
        public int StatusCode { get; }

        public UserFriendlyException0204068De1(string message)
            : base(message)
        {
            StatusCode = 400;
        }

        public UserFriendlyException0204068De1(string message, int statusCode)
            : base(message)
        {
            StatusCode = statusCode;
        }
    }
}