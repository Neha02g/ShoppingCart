using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EasyShop.ViewModel
{


public class AddProductModel
{
    [Required(ErrorMessage = "Product name is required")]
    [RegularExpression(@"^[A-Za-z -]{2,}$", ErrorMessage = "Product name must be at least 2 characters long and contain only letters.")]
    public string ProductName { get; set; }

    [Required(ErrorMessage = "Description is required")]
    [RegularExpression(@"^.{2,}$", ErrorMessage = "Description must be at least 2 characters long.")]
    public string Description { get; set; }
   
    [Required(ErrorMessage = "Price is required")]
    [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Price must be a valid number.")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "Category name is required")]
    [RegularExpression(@"^[A-Za-z -]{2,}$", ErrorMessage = "category name must be at least 2 characters long and contain only letters.")]
    public string CategoryName{ get; set; }

    [Required(ErrorMessage = "Quantity Available is required")]
    [RegularExpression(@"^\d+$", ErrorMessage = "Quantity Available must be a valid number.")]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity Available must be greater than zero.")]
    public int QuantityAvailable { get; set; }

    [Required(ErrorMessage = "Product Image must be a valid")]
    public string ProductImage { get; set; }
}
}