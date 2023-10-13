namespace Sakila_B.Core.Exceptions
{
    public class ExceptionResponse
    {
        public int Status { get; set; }

        public string Title { get; set; }

        public object Description { get; set; }
    }
}
