namespace ApiMessage.Models
{
    public class ApiResponse
    {
        public bool Success { get; set; } 
        public int Status {get; set;}
        public string? Message {get; set;}
    }
}
