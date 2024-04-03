using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EasyShop.Models
{

public class Cart
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CartID { get; set; }
    public int CustomerId { get; set; }
    public int ProductID { get; set; }
    public int Quantity { get; set; }

    [ForeignKey("CustomerId")]
    public Customer customer { get; set; }

    [ForeignKey("ProductID")]
    public Product Product { get; set; }
}
}