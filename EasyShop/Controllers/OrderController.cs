using EasyShop.IRepositories;
using EasyShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;


namespace EasyShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IOrderRepository orderRepository;
        public readonly ApplicationDbContext dbContext;

        public OrderController(IOrderRepository repo,ILogger<OrderController> logger,ApplicationDbContext context)
        {
            orderRepository = repo;
            _logger = logger;
            dbContext = context;
        }

[HttpPost]
[Route("Order")]
[Authorize(Roles = "Customer")]
public IActionResult Order()
{
    try{
    var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
    if (string.IsNullOrEmpty(userEmail))
    {
        return Unauthorized("User not authenticated");
    }

    var customer = orderRepository.FindCustomerByEmail(userEmail);
    if (customer == null)
    {
        return NotFound("Customer not found");
    }

      // Check if the customer already has an order with pending status
    var existingPendingOrder =orderRepository.FindPendingOrder(customer.CustomerId);

    if (existingPendingOrder != null)
    {
        // Remove all order items related to the existing pending order
       orderRepository.RemovePendingOrders(existingPendingOrder);
      //  return Ok("Existing pending order and its items removed. Please proceed with a new order.");
    }


    var cartItems = orderRepository.CartItems(customer.CustomerId);
        

    if (cartItems == null || !cartItems.Any())
    {
        return Ok("No items in cart to order.");
    }

    // Create a new order
    var order = new Order
    {
        CustomerId = customer.CustomerId,
        OrderStatus = "Pending", // Or any other initial status
        OrderDate = DateTime.Now
    };

   orderRepository.AddOrder(order);

    // Add order items from cart items
    foreach (var cartItem in cartItems)
    {
        var orderItem = new OrderItem
        {
            OrderID = order.OrderID,
            ProductID = cartItem.ProductID,
            Quantity = cartItem.Quantity,
            Price = cartItem.Product.Price
        };

       orderRepository.AddOrderItem(orderItem);
    }
    
    return Ok(); //"Please proceed with delivery details"
    }
    catch(Exception e){
                    _logger.LogError(e, "An error occurred while registering a new user.");
                    return StatusCode(500, "An unexpected error occurred while processing the request.");
        }
}

// Assuming you have appropriate relationships defined in your DbContext

[HttpGet]
[Route("PreviousOrders")]
[Authorize]
public IActionResult GetPreviousOrders()
{
    try{
    // Retrieve the authenticated user's ID
    var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;

    if (userEmail == null)
    {
        return BadRequest("User ID not found");
    }

    var customer = orderRepository.FindCustomerByEmail(userEmail);;
    if (customer == null)
    {
        return NotFound("Customer not found");
    }

    

      var paidOrders = dbContext.Orders
        .Join(dbContext.OrderItems,
            order => order.OrderID,
            orderItem => orderItem.OrderID,
            (order, orderItem) => new { Order = order, OrderItem = orderItem })
        .Join(dbContext.Transactions,
            orderOrderItem => orderOrderItem.Order.OrderID,
            transaction => transaction.OrderID,
            (orderOrderItem, transaction) => new { OrderOrderItem = orderOrderItem, Transaction = transaction })
        .Where(result => result.OrderOrderItem.Order.CustomerId == customer.CustomerId && result.OrderOrderItem.Order.OrderStatus == "Paid")
        .OrderByDescending(result => result.OrderOrderItem.Order.OrderDate)
        .Select(result => new
        {
            OrderId = result.OrderOrderItem.Order.OrderID,
            OrderDate = result.OrderOrderItem.Order.OrderDate,
            OrderTotal = result.OrderOrderItem.OrderItem.Quantity * result.OrderOrderItem.OrderItem.Price,
            OrderItems = new
            {
                ProductName = result.OrderOrderItem.OrderItem.Product.ProductName,
                Quantity = result.OrderOrderItem.OrderItem.Quantity,
                ProductImage = result.OrderOrderItem.OrderItem.Product.ProductImage
            },
            Transaction = new
            {
                TransactionId = result.Transaction.TransactionID,
                Amount = result.Transaction.Amount,
                PaymentMethod = result.Transaction.PaymentMethod
            }
        })
        .ToList();


    return Ok(paidOrders);
    }
    catch(Exception e){
                    _logger.LogError(e, "An error occurred while registering a new user.");
                    return StatusCode(500, "An unexpected error occurred while processing the request.");
        }
}



    }
}