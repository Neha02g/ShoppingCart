using Microsoft.AspNetCore.Mvc;
using EasyShop.ViewModel;
using EasyShop.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Net.Mail;
using System.Net;
using EasyShop.Repositories;
using EasyShop.IRepositories;




[ApiController]
[Route("api/[controller]")]
public class AuthController:ControllerBase{

private readonly ILogger<AuthController> _logger;
    private readonly IAuthRepository authRepository; // Add the DbContext here
     private readonly IConfiguration configuration; 

    public AuthController(IAuthRepository repo,IConfiguration configuration,ILogger<AuthController> logger)
    {
        this.authRepository = repo; // Initialize the dbContext
        this.configuration = configuration;
        _logger =logger;
    }


[HttpPost]
[Route("Register")]
public IActionResult Register([FromBody] RegisterModel model)
{
    try{
    if (!ModelState.IsValid)
    {
        return BadRequest("Invalid registration data");
    }

    // Check if a user with the same email already exists
    if(authRepository.emailExists(model.Email))
    {
        return BadRequest("Email address is already registered");
    }

    // Check if the provided phone number already exists
    if(authRepository.phoneNumberExists(model.PhoneNumber))
    {
        return BadRequest("Phone number is already registered");
    }

        var newCustomer = new Customer
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            PhoneNumber = model.PhoneNumber,
            Password = model.Password
        };

        // Add the new customer to your DbContext and save changes
        authRepository.addNewCustomer(newCustomer);
        SendRegisterEmail(model.Email,model.FirstName+model.LastName);

        return Ok();  //"User registered successfully"
    }
    catch(Exception e){
                    _logger.LogError(e, "An error occurred while registering a new user.");
                    return StatusCode(500, "An unexpected error occurred while processing the request.");
        }
    }


  //Loginusing JWT Token

    [HttpPost]
    [Route("Login")]
    public IActionResult Login([FromBody] LoginModel model)
    {
        try{
        if (!ModelState.IsValid)
        {
            return BadRequest("Invalid login data");
        }
        if(model.Email=="admin@gmail.com" && model.Password=="Admin@123")
        {
            LoginRoleModel l = new LoginRoleModel
            {
              Email= model.Email,
              Password = model.Password,
              Role="Admin"
            };
            var token = GenerateJwtToken(l);
            return Ok(new{message="Admin Logged in",Token=token});
        }
        if(authRepository.userExists(model))
        {
            LoginRoleModel l = new LoginRoleModel
            {
              Email= model.Email,
              Password = model.Password,
              Role="Customer"
            };
            var token = GenerateJwtToken(l);
            return Ok(new{message="Logged in",Token=token});
        }
        return BadRequest("Invalid Email or Password");
        }
          catch(Exception e){
                    _logger.LogError(e, "An error occurred while registering a new user.");
                    return StatusCode(500, "An unexpected error occurred while processing the request.");
        }
        
    }

    private string GenerateJwtToken(LoginRoleModel model)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));

        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
           new Claim(ClaimTypes.Role, model.Role),
            new Claim(ClaimTypes.Email, model.Email)
        };

        var token = new JwtSecurityToken(
            issuer: configuration["Jwt:Issuer"],
            audience: configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1), // Token expiration time
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private void SendRegisterEmail(string email,string name)
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
    Subject = "Welcome to EasyShop!",
    IsBodyHtml = true
};



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
            <h1>Welcome to EasyShop, " + name + @"!</h1>
            <p>Dear " + name + @",</p>
            <p>Thank you for registering with EasyShop. We're excited to have you on board!</p>
            <p>You are now part of a community that values convenience, quality, and excellent service.</p>
            <p>If you have any questions or need assistance, feel free to reach out to us.</p>
            <p>Happy shopping!</p>
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