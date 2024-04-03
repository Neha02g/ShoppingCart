using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EasyShop.ViewModel;
using EasyShop.Models;
using EasyShop.Repositories;





[ApiController]
[Route("api/[controller]")]
//[Authorize(Roles ="Admin")]
public class MerchantController:ControllerBase{

private readonly ILogger<MerchantController> _logger;
    private readonly IMerchantRepository merchantRepository; // Add the DbContext here
     private readonly IConfiguration configuration; 

    public MerchantController(IMerchantRepository repo,IConfiguration configuration,ILogger<MerchantController> logger)
    {
        this.merchantRepository = repo; // Initialize the dbContext
        this.configuration = configuration;
        _logger=logger;
    }

    [HttpGet("AllProducts")]
    public ActionResult<IEnumerable<Product>> GetAllProducts()
    {
        try{
       var allProducts = merchantRepository.GetAllProducts();
       if(allProducts!=null)
       return Ok(allProducts);
       return NotFound("No Prodcuts are present");
        }
        catch(Exception e){
                    _logger.LogError(e, "An error occurred while registering a new user.");
                    return StatusCode(500, "An unexpected error occurred while processing the request.");
        }
    }

    [HttpGet("ProductById")]
    public ActionResult<Category> ProductById(int id)
    {
        try{
       var product = merchantRepository.GetProductById(id);
       if(product!=null)
       return Ok(product);
       return NotFound("No product found");
       }
       catch(Exception e){
                    _logger.LogError(e, "An error occurred while registering a new user.");
                    return StatusCode(500, "An unexpected error occurred while processing the request.");
        }
    }

    [HttpPost("AddProduct")]
    public ActionResult AddProduct(AddProductModel model)
    {
        try{
        if(!ModelState.IsValid)
        {
            return BadRequest("Invalid data");
        }
       var category = merchantRepository.GetCategoryByName(model.CategoryName);
       if(category!=null)
       {
        var product = new Product{
            ProductName = model.ProductName,
            Description = model.Description,
            Price = model.Price,
            CategoryID = category.CategoryId,
            QuantityAvailable = model.QuantityAvailable,
            ProductImage = model.ProductImage
        };
        merchantRepository.AddProduct(product);
        return Ok("Product added SucessFully"); 
       }
       return BadRequest("category is not present,first add the category");  
        }
        catch(Exception e){
                    _logger.LogError(e, "An error occurred while registering a new user.");
                    return StatusCode(500, "An unexpected error occurred while processing the request.");
        }   
    }

   

    [HttpDelete("UpdateProduct")]
    public ActionResult UpdateProduct(int id, AddProductModel model)
    {
        try{
       if(!ModelState.IsValid)
        {
            return BadRequest("Invalid data");
        }
       var category = merchantRepository.GetCategoryByName(model.CategoryName);
       if(category!=null)
       {
       var product = merchantRepository.GetProductById(id);
       if(product.CategoryID!=category.CategoryId)
       return BadRequest("You cannot update the Category");
      
       product.ProductName = model.ProductName;
       product.Description = model.Description;
       product.Price = model.Price;
       product.CategoryID = category.CategoryId;
       product.QuantityAvailable = model.QuantityAvailable;
       product.ProductImage = model.ProductImage;
    
        merchantRepository.SaveChange();
        return Ok("Product updates SucessFully"); 
       }
       return BadRequest("Not category present,first add the category"); 
        }
        catch(Exception e){
                    _logger.LogError(e, "An error occurred while registering a new user.");
                    return StatusCode(500, "An unexpected error occurred while processing the request.");
        }
    }
}
