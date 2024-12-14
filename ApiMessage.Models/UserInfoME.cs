using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiMessage.Models
{
    public class UserInfoME
    {
        public int UserId {get; set;}
        public string? FullName {get; set;}
        public string? PhoneNumber { get; set;}
        public string? Email { get; set; }
    }
}
