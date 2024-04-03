using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EasyShop.Models
{

public class DeliveryDetail
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int DeliveryID { get; set; }
    public int OrderID { get; set; }
    public string FullName { get; set; }
    public string Address { get; set; }
    public string Pincode { get; set; }
    public string MobileNumber { get; set; }

    [ForeignKey("OrderID")]
    public Order Order { get; set; }
}
}