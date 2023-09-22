using FluentValidation.Results;
using SakilaAPI.Core.Contants;

namespace SakilaAPI.Core.Exceptions
{
    /// <summary>
    /// client error exception
    /// </summary>
    public class StatusClientErrorException : CustomException
    {
        /// <summary>
        /// dictionary errors
        /// </summary>
        public IDictionary<string, string[]> Errors { get; set; } = new Dictionary<string, string[]>();

        /// <summary>
        /// Khởi tạo exception với status code 400
        /// </summary>
        public StatusClientErrorException() : base(MessageSystem.DATA_INVALID)
        {
            Code = StatusCodes.Status400BadRequest;
        }

        /// <summary>
        /// Khởi tạo exception với danh sách lỗi
        /// </summary>
        /// <param name="errors"></param>
        public StatusClientErrorException(IDictionary<string, string[]> errors) : base(MessageSystem.DATA_INVALID)
        {
            Errors = errors;
            Code = StatusCodes.Status400BadRequest;
        }

        /// <summary>
        /// Triển khai cấu hính status code với validation
        /// </summary>
        /// <param name="failures"></param>
        public StatusClientErrorException(IEnumerable<ValidationFailure> failures) : base(MessageSystem.DATA_INVALID)
        {
            Code = StatusCodes.Status400BadRequest;
            var failureGroups = failures.GroupBy(e => e.PropertyName, e => e.ErrorMessage);

            foreach (var failureGroup in failureGroups)
            {
                Errors.Add(failureGroup.Key, failureGroup.ToArray());
            }
        }
    }
}
