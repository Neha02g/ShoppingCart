using System.ComponentModel.DataAnnotations;

namespace EasyShop.ViewModel
{
public class ProfileDataModel
{
    [Required(ErrorMessage = "First name is required")]
    [RegularExpression(@"^[A-Za-z]{2,}$", ErrorMessage = "First name must be at least 2 characters long and contain only letters.")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Last name is required")]
    [RegularExpression(@"^[A-Za-z]{2,}$", ErrorMessage = "Last name must be at least 2 characters long and contain only letters.")]
    public string LastName { get; set; }


    [Required(ErrorMessage = "Phone number is required")]
    [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be 10 digits long.")]
    public string PhoneNumber { get; set; }
}
}
