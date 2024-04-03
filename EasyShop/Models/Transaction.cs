using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EasyShop.Models
{

public class Transaction
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int TransactionID { get; set; }
    public int OrderID { get; set; }
    public decimal Amount { get; set; }
    public string PaymentMethod { get; set; }

    [ForeignKey("OrderID")]
    public Order Order { get; set; }
}
}