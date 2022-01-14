using System.ComponentModel.DataAnnotations;
using System.ComponentModel;


namespace appv1.DAL.Models
{
    public class Login
    {

        public int ID { get; set; }


        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DefaultValue(0)]
        public byte Admin { get; set; }
    }
}