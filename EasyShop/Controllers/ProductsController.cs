using EasyShop.IRepositories;
using EasyShop.Models;
using EasyShop.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly  IProductRepository productRepository;

        public ProductsController(IProductRepository repo,ILogger<ProductsController> logger)
        {
            productRepository = repo;
            _logger = logger;
        }
      

    [HttpGet("search")]
    public ActionResult<IEnumerable<Product>> GetAllProducts()
    {
        try{
        var allProducts = productRepository.GetAllProducts();
        if(allProducts!=null)
        return Ok(allProducts);
        return NotFound("No Products are present"); 
        }
        catch(Exception e){
                    _logger.LogError(e, "An error occurred while registering a new user.");
                    return StatusCode(500, "An unexpected error occurred while processing the request.");
        }
    } 
        

    // GET: api/products/search
    [HttpGet("searchBy")]
     public async Task<ActionResult<IEnumerable<Product>>> SearchProducts(string query)
    {
        try{
    if (string.IsNullOrEmpty(query))
    {
        return BadRequest("Search query cannot be empty.");
    }

    var lowerCaseQuery = query.ToLower(); // Convert query to lowercase

    var products = productRepository.GetAllProductsBySearch(lowerCaseQuery);

    if (products == null || products.Count == 0)
    {
        return NotFound("No products found matching the search query.");
    }

    return Ok(products);
        }
        catch(Exception e){
                    _logger.LogError(e, "An error occurred while registering a new user.");
                    return StatusCode(500, "An unexpected error occurred while processing the request.");
        }
   }

    }

   
}
