using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EasyShop.Models;
using EasyShop.IRepositories;





[ApiController]
[Route("api/[controller]")]
[Authorize(Roles ="Admin")]
public class CategoryController:ControllerBase{

private readonly ILogger<CategoryController> _logger;
    private readonly ICategoryRepository categoryRepository; // Add the DbContext here
     private readonly IConfiguration configuration; 

    public CategoryController(ICategoryRepository repo,IConfiguration configuration,ILogger<CategoryController> logger)
    {
        this.categoryRepository =repo; // Initialize the dbContext
        this.configuration = configuration;
        _logger = logger;
    }

    [HttpGet("AllCategories")]
    public ActionResult<IEnumerable<Category>> GetAllCategories()
    {
      try{
       var allCategories = categoryRepository.getAllCategories();
       if(allCategories!=null)
       return Ok(allCategories);
       return NotFound("No categories are present");
      }
      catch(Exception e){
                    _logger.LogError(e, "An error occurred while registering a new user.");
                    return StatusCode(500, "An unexpected error occurred while processing the request.");
        }
    }

    [HttpGet("CategoryById")]
    public ActionResult<Category> CategoryById(int id)
    {
      try{
       var category = categoryRepository.GetCategoryById(id);
       if(category!=null)
       return Ok(category);
       return NotFound("No category found");
      }
      catch(Exception e){
                    _logger.LogError(e, "An error occurred while registering a new user.");
                    return StatusCode(500, "An unexpected error occurred while processing the request.");
        }
    }

    [HttpPost("AddCategory")]
    public ActionResult AddCategory(string Name,string Description)
    {
      try{
       if(categoryRepository.FindCategory(Name))
       return BadRequest("category is already present");
       categoryRepository.AddCategory(Name,Description);
      return Ok("Category added SucessFully"); 
      }
      catch(Exception e){
                    _logger.LogError(e, "An error occurred while registering a new user.");
                    return StatusCode(500, "An unexpected error occurred while processing the request.");
        }
    }

    
}

//  [HttpPut("UpdateCategory")]
//     public ActionResult UpdateCategory(string Name,string Description)
//     {
//        if(dbContext.Categories.Any(x=>x.Name == Name))
//        {
//         var alreadyPresent = dbContext.Categories.FirstOrDefault(x=>x.Name == Name);
//          alreadyPresent.Description = Description;
//          dbContext.SaveChanges();
//          return Ok("Description of Category updated SucessFully"); 
//        }
//        else if(dbContext.Categories.Any(x=>x.Description == Description))
//        {
//         var alreadyPresent = dbContext.Categories.FirstOrDefault(x=>x.Description == Description);
//          alreadyPresent.Name = Name;
//          dbContext.SaveChanges();
//          return Ok("Name of Category updated SucessFully"); 
//        }
//        return NotFound("Category does not match");
//     }

//     [HttpGet("DeleteCategory")]
//     public ActionResult DeleteCategory(int id)
//     {
//      if(dbContext.Categories.Any(x=>x.CategoryId==id))
//      {
//         if(dbContext.Products.Any())
//      }
//        var allCategories = dbContext.Categories.ToList();
//        if(allCategories!=null)
//        return Ok(allCategories);
//        return NotFound("No categories are present");
//     }