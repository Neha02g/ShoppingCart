using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EasyShop.Models;
using System.Security.Claims;
using System.Data;
using EasyShop.Repositories;
using EasyShop.IRepositories;


[ApiController]
[Route("api/[controller]")]
public class CartController : ControllerBase
{
    private readonly ILogger<CartController> _logger;
    private readonly ICartRepository cartRepository;

    public CartController(ICartRepository repo,ILogger<CartController> logger)
    {
        this.cartRepository = repo;
        _logger = logger;
    }

    [HttpGet]
    [Route("ViewCart")]
    [Authorize(Roles = "Customer")]
    public IActionResult ViewCart()
    {
        try{
        var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
        if (string.IsNullOrEmpty(userEmail))
        {
            return Unauthorized("User not authenticated");
        }

        var customer = cartRepository.FindCustomerByEmail(userEmail);
        if (customer == null)
        {
            return NotFound("Customer not found");
        }

        var items = cartRepository.CartData(customer.CustomerId);
        if (items == null || !items.Any())
        {
            return Ok("OOPS! Cart is Empty");
        }

        return Ok(items);
        }
          catch(Exception e){
                    _logger.LogError(e, "An error occurred while registering a new user.");
                    return StatusCode(500, "An unexpected error occurred while processing the request.");
        }
    }

    [HttpPost]
    [Route("AddToCart/{id}")]
    [Authorize(Roles = "Customer")]
    public IActionResult AddToCart(int id)
    {
        try{
        var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
        if (string.IsNullOrEmpty(userEmail))
        {
            return Unauthorized("User not authenticated");
        }

        var customer = cartRepository.FindCustomerByEmail(userEmail);
        if (customer == null)
        {
            return NotFound("Customer not found");
        }

        var product = cartRepository.FindProductById(id);
        if (product == null)
        {
            return NotFound("Product not found");
        }
        if(product.QuantityAvailable < 1){
            return NotFound("Oops! Quantity Limit Exceed!! ");
        }
        var alreadyPresent =cartRepository.ItemAlreadyPresent(customer.CustomerId,id);
        if(alreadyPresent!=null)
        {
          cartRepository.IncreaseQuantity(alreadyPresent);
        }
        else
        {
            var cartItem = new Cart
           {
            CustomerId = customer.CustomerId,
            ProductID = id,
            Quantity = 1
           };
         cartRepository.AddItemToCart(cartItem);
        }

        return Ok(); //"Item added to cart successfully"
        }
       
     catch(Exception e){
                    _logger.LogError(e, "An error occurred while registering a new user.");
                    return StatusCode(500, "An unexpected error occurred while processing the request.");
        }
    }


    [HttpGet]
[Route("CartSummary")]
[Authorize(Roles = "Customer")]
public IActionResult CartSummary()
{
    try{
    var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
    if (string.IsNullOrEmpty(userEmail))
    {
        return Unauthorized("User not authenticated");
    }

    var customer = cartRepository.FindCustomerByEmail(userEmail);
    if (customer == null)
    {
        return NotFound("Customer not found");
    }

    var cartItems = cartRepository.CartData(customer.CustomerId);

    if (cartItems == null || !cartItems.Any())
    {
        return Ok("No items in cart, so no price.");
    }
    int totalItems =0;
    foreach(var i in cartItems)
    {
        totalItems+=i.Quantity;
    }

    var itemsWithPrice = cartItems.Select(cartItem => new
    {
        Name = cartItem.Product.ProductName,
        ProductQuantity = cartItem.Quantity,
        PricePerItem = cartItem.Product.Price,
        TotalPriceForItem = cartItem.Quantity * cartItem.Product.Price
    }).ToList();

    decimal totalPrice = itemsWithPrice.Sum(item => item.TotalPriceForItem);

    var result = new
    {
        Items = itemsWithPrice,
        TotalItems = totalItems,
        TotalPrice = totalPrice
    };

    return Ok(result);
    }
    catch(Exception e){
                    _logger.LogError(e, "An error occurred while registering a new user.");
                    return StatusCode(500, "An unexpected error occurred while processing the request.");
        }
}

[HttpPost]
[Route("RemoveFromCart/{id}")]
[Authorize(Roles = "Customer")]
public IActionResult RemoveFromCart(int id)
{
    try{
    var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
    if (string.IsNullOrEmpty(userEmail))
    {
        return Unauthorized("User not authenticated");
    }

    var customer = cartRepository.FindCustomerByEmail(userEmail);
    if (customer == null)
    {
        return NotFound("Customer not found");
    }

    var cartItem = cartRepository.ItemAlreadyPresent(customer.CustomerId,id);

    if (cartItem == null)
    {
        return NotFound("Item not found in cart");
    }

    if(1 > cartItem.Quantity){
        return NotFound("Please check the Quantity Value");
    }

    // If the requested quantity to remove is greater than the quantity in the cart, remove the entire item
    if (cartItem.Quantity <= 1)
    {
        cartRepository.RemoveItemFromCart(cartItem);
    }
    else
    {
        cartRepository.ReduceQuantity(cartItem);
    }

    return Ok(); //"Item removed from cart successfully"

}
catch(Exception e){
                    _logger.LogError(e, "An error occurred while registering a new user.");
                    return StatusCode(500, "An unexpected error occurred while processing the request.");
        }

}
}
