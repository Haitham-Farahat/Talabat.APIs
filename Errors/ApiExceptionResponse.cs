namespace Talabat.APIs.Errors
{
    public class ApiExceptionResponse : ApiResponse
    {
        public string? Details { get; set; }
        public ApiExceptionResponse(int StatuesCode, string? Message = null, string? details = null) : base(StatuesCode, Message)
        {
            Details = details;
        }
    }
}
