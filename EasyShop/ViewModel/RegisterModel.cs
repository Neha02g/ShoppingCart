using System.ComponentModel.DataAnnotations;

namespace EasyShop.ViewModel
{
public class RegisterModel
{
    [Required(ErrorMessage = "First name is required")]
    [RegularExpression(@"^[A-Za-z]{2,}$", ErrorMessage = "First name must be at least 2 characters long and contain only letters.")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Last name is required")]
    [RegularExpression(@"^[A-Za-z]{2,}$", ErrorMessage = "Last name must be at least 2 characters long and contain only letters.")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [RegularExpression(@"^(?=.{1,256}$)[a-zA-Z0-9_-]+(?:\.[a-zA-Z0-9_-]+)*@gmail\.com$",ErrorMessage ="Email Address is not Correct")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Phone number is required")]
    [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be 10 digits long.")]
    public string PhoneNumber { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
     [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{6,}$", ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one digit, one special character, and be at least 6 characters long.")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Confirm password is required")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Passwords do not match")]
    public string ConfirmPassword { get; set; }
}
}
