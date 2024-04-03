using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EasyShop.Models
{


public class Product
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ProductID { get; set; }
    public string ProductName { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int CategoryID { get; set; }
    public int QuantityAvailable { get; set; }
    public string ProductImage { get; set; }

    [ForeignKey("CategoryID")]
    public Category Category { get; set; }
}
}