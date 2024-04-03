using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using EasyShop.Models;
using System.Net.Mail;
using System.Net;
using EasyShop.IRepositories;

[ApiController]
[Route("api/[controller]")]
public class TransactionController : ControllerBase
{
    private readonly ITransactionRepository transactionRepository;
    private readonly ILogger<TransactionController> _logger;

    public TransactionController(ITransactionRepository repo,ILogger<TransactionController> logger)
    {
        transactionRepository = repo;
        _logger = logger;
    }

    [HttpPost]
    [Route("PaymentMethod")]
    [Authorize(Roles = "Customer")]
    public IActionResult PaymentMethod(string PaymentMethod)
    {
        try{
        if (PaymentMethod.ToLower()!="cod" && PaymentMethod.ToLower()!="card")
        {
            return BadRequest("Invalid payment method, choose from(cod/card)");
        }

        var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
        if (string.IsNullOrEmpty(userEmail))
        {
            return Unauthorized("User not authenticated");
        }

        var customer = transactionRepository.FindCustomerByEmail(userEmail);
        if (customer == null)
        {
            return NotFound("Customer not found");
        }

        var order = transactionRepository.FindPendingOrder(customer.CustomerId);

        if (order == null)
        {
            return NotFound("No pending orders found for this customer");
        }

        // Create a new transaction entry
        var transaction = new Transaction
        {
            OrderID = order.OrderID,
            Amount = CalculateOrderTotal(order.OrderID), // Calculate order total amount
            PaymentMethod = PaymentMethod
        };

         transactionRepository.Addtransaction(transaction);
         // Update order status to reflect payment made and date
        order.OrderStatus = "Paid";
        order.OrderDate = DateTime.Now;
        
        // Remove cart items associated with the completed order
        //var cartItemsToRemove = dbContext.Carts.Where(c => c.CustomerId == customer.CustomerId && c.Product.CategoryID == order.OrderID).ToList();
        var cartItemsToRemove = transactionRepository.CartItems(customer.CustomerId);
        
        // remove quantity of the chosen products
        // Decrease the quantity of each product in inventory
       foreach (var cartItem in cartItemsToRemove)
      {
          var product = cartItem.Product;

       // Decrease the quantity in inventory
         product.QuantityAvailable -= cartItem.Quantity;
     }
        // Remove the cart item
        transactionRepository.RemoveCartItems(cartItemsToRemove);
        transactionRepository.SaveChange();
        SendOrderEmail(customer.Email,order.OrderID);
        return Ok(); //"Order Placed Successfully"
        }
        catch(Exception e){
                    _logger.LogError(e, "An error occurred while registering a new user.");
                    return StatusCode(500, "An unexpected error occurred while processing the request.");
        }
    }

    private decimal CalculateOrderTotal(int orderId)
    {
        return transactionRepository.CalculateOrderTotal(orderId);
    }
    private void SendOrderEmail(string email,int orderId)
    {
        // Send OTP email logic here
        var smtpClient = new SmtpClient("smtp.gmail.com")
        {
            Port = 587,
            Credentials = new NetworkCredential("pranjilgupta0216@gmail.com", "wokriwtsctgzkyfm"),
            EnableSsl = true,
        };

        var mailMessage = new MailMessage
{
    From = new MailAddress("pranjilgupta0216@gmail.com"),
    Subject = "Thank You for Shopping at EasyShop!",
    IsBodyHtml = true
};
var address = transactionRepository.GetAddress(orderId);
// Delivery address (assuming you have it)
string deliveryAddress = address.Address+" "+address.Pincode;

// Body of the email
string body = @"
    <html>
    <head>
        <style>
            /* Add your CSS styles here for a beautiful email layout */
            body {
                font-family: Arial, sans-serif;
                background-color: #f4f4f4;
                padding: 20px;
            }
            .container {
                max-width: 600px;
                margin: 0 auto;
                background-color: #fff;
                border-radius: 10px;
                padding: 20px;
                box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
            }
            h1 {
                color: #333;
            }
            p {
                color: #666;
            }
        </style>
    </head>
    <body>
        <div class='container'>
            <h1>Thank You for Shopping at EasyShop!</h1>
            <p>Dear Valued Customer,</p>
            <p>We wanted to take a moment to express our gratitude for choosing EasyShop for your recent purchase.</p>
            <p>Your order has been successfully placed. Here are the details:</p>
            <ul>
                <!-- Add order details here -->
                <li>Order Placed: March 14, 2024</li>
                <li>Delivery Address: " + deliveryAddress + @"</li>
                <!-- Add more order details as needed -->
            </ul>
            <p>If you have any questions or concerns regarding your order, feel free to contact us.</p>
            <p>Thank you once again for choosing EasyShop. We look forward to serving you again soon!</p>
            <p>Best Regards,<br/>The EasyShop Team</p>
        </div>
    </body>
    </html>";

      // Set the email body
       mailMessage.Body = body;


        mailMessage.To.Add(email);

        smtpClient.Send(mailMessage);
    }
}


