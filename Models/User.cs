using System.ComponentModel.DataAnnotations;

namespace UserProfile.Models
{
    public class User
    {
        public Guid Id { get; set; }   
        public string Name { get; set; }
        public byte[]? Picture { get; set; }
       
        [Required]
        [StringLength(20)]
        public string Login { get; set; }

        [Required]        
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; }


    }
}
