using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EasyShop.ViewModel;
using EasyShop.Models;
using System.Security.Claims;
using System.Data;
using EasyShop.IRepositories;


[ApiController]
[Route("api/[controller]")]
public class DeliveryController : ControllerBase
{
    private readonly ILogger<DeliveryController> _logger;
    private readonly IDeliveryRepository deliveryRepository;

    public DeliveryController(IDeliveryRepository repo,ILogger<DeliveryController> logger)
    {
        this.deliveryRepository = repo;
        _logger = logger;
    }

    [HttpPost]
    [Route("AddDeliveryDetails")]
    [Authorize(Roles = "Customer")]
    public IActionResult AddDeliveryDetails([FromBody] DeliveryDetailModel deliveryDetailModel)
    {
        try{
        if (!ModelState.IsValid)
        {
            return BadRequest("Invalid delivery details");
        }

        var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
        if (string.IsNullOrEmpty(userEmail))
        {
            return Unauthorized("User not authenticated");
        }

        var customer = deliveryRepository.FindCustomerByEmail(userEmail);
        if (customer == null)
        {
            return NotFound("Customer not found");
        }

        var order = deliveryRepository.FindPendingOrder(customer.CustomerId);

        if (order == null)
        {
            return NotFound("No pending orders found for this customer");
        }

        // Check if delivery details already exist for the given order ID
    var existingDeliveryDetail = deliveryRepository.ExistingDeliveryDetails(order.OrderID);

    if (existingDeliveryDetail != null)
    {
        // Update the existing delivery details
       deliveryRepository.UpdateDeliveryDetails(deliveryDetailModel,existingDeliveryDetail);

        return Ok(); //"Delivery details added successfully"
    }

        var deliveryDetail = new DeliveryDetail
        {
            OrderID = order.OrderID,
            FullName = deliveryDetailModel.FullName,
            Address = deliveryDetailModel.Address,
            Pincode = deliveryDetailModel.Pincode,
            MobileNumber = deliveryDetailModel.MobileNumber
        };

        deliveryRepository.AddNewDeliveryDetails(deliveryDetail);

        return Ok(); //"Delivery details added successfully"
        }
        catch(Exception e){
                    _logger.LogError(e, "An error occurred while registering a new user.");
                    return StatusCode(500, "An unexpected error occurred while processing the request.");
        }
    }

}