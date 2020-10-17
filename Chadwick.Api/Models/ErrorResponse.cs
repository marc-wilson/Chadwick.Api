namespace Chadwick.Api.Models
{
    /// <summary>
    /// Generic Error Response
    /// </summary>
    public class ErrorResponse
    {
        /// <summary>
        /// Error Message
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Error response with message
        /// </summary>
        /// <param name="message"></param>
        public ErrorResponse(string message)
        {
            Message = message;
        }
    }
}