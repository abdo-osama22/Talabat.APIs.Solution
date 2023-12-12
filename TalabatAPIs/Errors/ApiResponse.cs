namespace TalabatAPIs.Errors
{
    public class ApiResponse
    {
        public int StatusCode{ get; set; }
        public string? Message{ get; set; }

        public ApiResponse(int statusCode, string? message= null)
        {
            StatusCode = statusCode;
            Message = message?? GetDefaultMessage(statusCode);
        }

        private string? GetDefaultMessage(int status)
        {
            return StatusCode switch
            {
                400 => "Bad Request",
                401 => "Your are not Authorized",
                404 => "Resource Not Found",
                500 => "Internal Server Error",
                 _  =>  null
            };
        }
    }

}
