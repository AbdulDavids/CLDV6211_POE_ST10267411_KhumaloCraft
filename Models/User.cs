using CLDV6211_POE_ST10267411_KhumaloCraft.Data;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace CLDV6211_POE_ST10267411_KhumaloCraft.Models
{
    public class User
    {
        public int UserID { get; set; }

        [Required]
        [UniqueUserName]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}




public class UniqueUserNameAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var context = (AppDbContext)validationContext.GetService(typeof(AppDbContext));
        var userName = value as string;

        if (context.Users.Any(u => u.UserName == userName))
        {
            return new ValidationResult("Username is already taken.");
        }

        return ValidationResult.Success;
    }
}

