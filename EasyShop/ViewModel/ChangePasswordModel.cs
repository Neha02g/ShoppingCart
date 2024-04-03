using System.ComponentModel.DataAnnotations;

namespace EasyShop.ViewModel
{
public class ChangePasswordModel
{

    [Required(ErrorMessage = "Email is required")]
    [RegularExpression(@"^(?=.{1,256}$)[a-zA-Z0-9_-]+(?:\.[a-zA-Z0-9_-]+)*@gmail\.com$",ErrorMessage ="Email Address is not Correct")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email { get; set; }


    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
     [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{6,}$", ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one digit, one special character, and be at least 6 characters long.")]
    public string NewPassword { get; set; }

    [Required(ErrorMessage = "Confirm password is required")]
    [DataType(DataType.Password)]
    [Compare("NewPassword", ErrorMessage = "Passwords do not match")]
    public string ConfirmPassword { get; set; }
}
}
