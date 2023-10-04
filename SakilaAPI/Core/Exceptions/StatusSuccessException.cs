using SakilaAPI.Core.Contants;

namespace SakilaAPI.Core.Exceptions
{
    /// <summary>
    /// Exception case success
    /// </summary>
    public class StatusSuccessException : CustomException
    {
        /// <summary>
        /// Contructor StatusSuccessException
        /// </summary>
        public StatusSuccessException() : base(MessageSystem.NO_DATA)
        {
            Code = StatusCodes.Status200OK;
        }

        /// <summary>
        /// Contructor with title description
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="title"></param>
        /// <param name="description"></param>
        public StatusSuccessException(int statusCode, string title, string description) : base(statusCode, title, description)
        {}

        /// <summary>
        /// Contructor with title description
        /// </summary>
        /// <param name="title"></param>
        /// <param name="description"></param>
        public StatusSuccessException(string title, string description) : base(StatusCodes.Status200OK, title, description)
        { }

        /// <summary>
        /// Contructor with title descriptions
        /// </summary>
        /// <param name="title"></param>
        /// <param name="descriptions"></param>
        public StatusSuccessException(string title, object[] descriptions) : base(StatusCodes.Status200OK, title, descriptions)
        {
        }
    }
}
