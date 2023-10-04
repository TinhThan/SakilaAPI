using SakilaAPI.Core.Contants;

namespace SakilaAPI.Core.Exceptions
{
    /// <summary>
    /// exception trả về khi code error
    /// </summary>
    public class StatusServerErrorException : CustomException
    {
        /// <summary>
        /// Contrustor
        /// </summary>
        public StatusServerErrorException() : base(MessageSystem.NO_DATA)
        {
            Code = StatusCodes.Status500InternalServerError;
        }

        public StatusServerErrorException(string title) : base(title)
        {
            Code = StatusCodes.Status500InternalServerError;
            Title = title;
        }

        /// <summary>
        /// Contructor with title description
        /// </summary>
        /// <param name="title"></param>
        /// <param name="description"></param>
        public StatusServerErrorException(string title, string description) : base(StatusCodes.Status500InternalServerError, title, description) { }

        /// <summary>
        /// Contructor with title descriptions
        /// </summary>
        /// <param name="title"></param>
        /// <param name="descriptions"></param>
        public StatusServerErrorException(string title, object[] descriptions) : base(StatusCodes.Status500InternalServerError, title, descriptions)
        {
            Code = StatusCodes.Status500InternalServerError;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="title"></param>
        /// <param name="descriptions"></param>
        public StatusServerErrorException(int code, string title, object[] descriptions) : base(code, title, descriptions) { }
    }
}
