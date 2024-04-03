using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EasyShop.Models
{

public class Order
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int OrderID { get; set; }
    public int CustomerId { get; set; }
    public string OrderStatus { get; set; }
    public DateTime OrderDate { get; set; }

    [ForeignKey("CustomerId")]
    public Customer customer { get; set; }
}
}