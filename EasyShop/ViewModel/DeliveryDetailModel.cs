using System.ComponentModel.DataAnnotations;

namespace EasyShop.ViewModel
{
public class DeliveryDetailModel
{
    [Required(ErrorMessage = "First name is required")]
    [RegularExpression(@"^[A-Za-z ]{2,}$", ErrorMessage = "Name must be at least 2 characters long and contain only letters.")]

    public string FullName { get; set; }

    [Required(ErrorMessage ="Address is Required")]
    public string Address { get; set; }

    [Required(ErrorMessage = "PinCode is required")]
    [RegularExpression(@"^[0-9]{6}$", ErrorMessage = "Pincode must be 6 digits long")]
    public string Pincode { get; set; }

    [Required(ErrorMessage = "Mobile number is required")]
    [RegularExpression(@"^\d{10}$", ErrorMessage = "Mobile number must be 10 digits long.")]
    public string MobileNumber { get; set; }
}
}