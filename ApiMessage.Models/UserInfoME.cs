using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiMessage.Models
{
    //[Table("UserInfoME", Schema = "USU")]
    public class UserInfoME
    {
        //[Key]
        //[Column("UserId")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId {get; set;}

        //[Column("FullName")]
        //[MaxLength(100)]
        public string? FullName {get; set;}

        //[Column("PhoneNumber")]
        //[MaxLength(15)]
        public string? PhoneNumber { get; set;}

        //[Column("Email")]
        //[MaxLength(60)]
        public string? Email { get; set; }
    }
}
