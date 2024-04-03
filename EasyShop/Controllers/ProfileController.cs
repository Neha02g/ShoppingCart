using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EasyShop.ViewModel;

using System.Security.Claims;
using EasyShop.IRepositories;



[ApiController]
[Route("api/[controller]")]
public class ProfileController:ControllerBase{

private readonly ILogger<ProfileController> _logger;

    private readonly IProfileRepository profileRepository; // Add the DbContext here
     private readonly IConfiguration configuration; 

    public ProfileController(IProfileRepository repo,IConfiguration configuration,ILogger<ProfileController> logger)
    {
        this.profileRepository = repo; // Initialize the dbContext
        this.configuration = configuration;
        _logger = logger;
    }

    [HttpPost("UpdateProfile")]
    [Authorize(Roles ="Customer")]
    public ActionResult UpdateProfile(ProfileDataModel profile)
    {
        try{
         var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
        if (string.IsNullOrEmpty(userEmail))
        {
            return Unauthorized("User not authenticated");
        }

        var customer = profileRepository.FindCustomerByEmail(userEmail);
        if (customer == null)
        {
            return NotFound("Customer not found");
        }
        else if(profileRepository.FindCustomerByPhoneNumber(profile.PhoneNumber))
        {
          return BadRequest("Mobile Number is already registered");
        }
        else{

            profileRepository.UpdateCustomerData(customer,profile);
            return Ok();  //"Profile Update SuccessFully"
        }
        }
        catch(Exception e){
                    _logger.LogError(e, "An error occurred while registering a new user.");
                    return StatusCode(500, "An unexpected error occurred while processing the request.");
        }
    }

    [HttpGet("GetProfileData")]
    [Authorize(Roles ="Customer")]
    public ActionResult GetProfileData()
    {
        try{
         var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
        if (string.IsNullOrEmpty(userEmail))
        {
            return Unauthorized("User not authenticated");
        }

        var customer = profileRepository.FindCustomerByEmail(userEmail);
        if (customer == null)
        {
            return NotFound("Customer not found");
        }

        var profileData = new ProfileDataModel{
        FirstName = customer.FirstName,
        LastName = customer.LastName,
        PhoneNumber = customer.PhoneNumber
        };
        return Ok(profileData);
        }
        catch(Exception e){
                    _logger.LogError(e, "An error occurred while registering a new user.");
                    return StatusCode(500, "An unexpected error occurred while processing the request.");
        }
        }
}
