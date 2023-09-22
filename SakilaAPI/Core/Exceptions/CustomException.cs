namespace SakilaAPI.Core.Exceptions
{
    /// <summary>
    /// Custom exception
    /// </summary>
    public class CustomException : Exception
    {
        /// <summary>
        /// property code
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// property title
        /// </summary>
        public string? Title { get;set; }

        /// <summary>
        /// description
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Descriptions
        /// </summary>
        public object[]? Descriptions { get; set; }

        /// <summary>
        /// Khởi tạo exception
        /// </summary>
        public CustomException() : base()
        {

        }

        /// <summary>
        /// Khởi tạo exception with message
        /// </summary>
        /// <param name="message"></param>
        public CustomException(string message) : base(message)
        {
        }

        /// <summary>
        /// Khởi tạo exception
        /// </summary>
        /// <param name="code"></param>
        /// <param name="title"></param>
        /// <param name="description"></param>
        public CustomException(int code, string title, string description) : base(title)
        {
            Code = code;
            Title = title;
            Description = description;
        }

        /// <summary>
        /// Khởi tạo exception
        /// </summary>
        /// <param name="code"></param>
        /// <param name="title"></param>
        /// <param name="descriptions"></param>
        public CustomException(int code, string title, object[] descriptions) : base(title)
        {
            Code = code;
            Title = title;
            Descriptions = descriptions;
        }
    }
}
