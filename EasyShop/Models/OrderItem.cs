using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EasyShop.Models
{

public class OrderItem
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int OrderItemID { get; set; }
    public int OrderID { get; set; }
    public int ProductID { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }

    [ForeignKey("OrderID")]
    public Order Order { get; set; }

    [ForeignKey("ProductID")]
    public Product Product { get; set; }
}
}