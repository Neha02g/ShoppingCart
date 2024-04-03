using Microsoft.AspNetCore.Mvc;
using EasyShop.Repositories;
using System.Net;
using System.Net.Mail;
using EasyShop.ViewModel;
using System.Text;


[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly IUserService _userService;
    
    //private string _otp;
    public UserController(IUserService userService,ILogger<UserController> logger)
    {
        _userService = userService;
        _logger = logger;
    }

    [HttpPost("forgot-password")]
    public IActionResult ForgotPassword(string email)
    {
        try{
        var result = _userService.ForgotPassword(email.ToLower());
        if (result)
        {
            return Ok(); //"Password reset instructions sent to your email."
        }
        else
        {
            return NotFound("User not found.");
        }
        }
        catch(Exception e){
                    _logger.LogError(e, "An error occurred while registering a new user.");
                    return StatusCode(500, "An unexpected error occurred while processing the request.");
        }
    }

    [HttpPost("verify-otp")]
    public IActionResult VerifyOTP(string email, string otp)
    {
        try{
        var result = _userService.VerifyOTP(email.ToLower(), otp);
        // if(otp!=_otp)
        // return BadRequest("Invalid OTP.");
        if (result)
        {
            return Ok(); //"OTP verified successfully."
        }
        else
        {
            return BadRequest("Invalid Gmail or OTP.");
        }
        }
        catch(Exception e){
                    _logger.LogError(e, "An error occurred while registering a new user.");
                    return StatusCode(500, "An unexpected error occurred while processing the request.");
        }
    }

    [HttpPost("reset-password")]
    public IActionResult ResetPassword(ChangePasswordModel model)
    {
        try{
        var result = _userService.ResetPassword(model.Email.ToLower(), model.NewPassword,model.ConfirmPassword);
        if (result)
        {
            return Ok();  //"Password reset successfully."
        }
        else
        {
            return BadRequest("Failed to reset password. Mail is Incorrect");
        }
        }
        catch(Exception e){
                    _logger.LogError(e, "An error occurred while registering a new user.");
                    return StatusCode(500, "An unexpected error occurred while processing the request.");
        }
    }
}

// UserService.cs

public interface IUserService
{
    bool ForgotPassword(string email);
    bool VerifyOTP(string email, string otp);
    bool ResetPassword(string email, string newPassword, string ConfirmPassword);
}

public class OTPData
    {
        public string OTP { get; set; }
        public DateTime ExpirationTime { get; set; }
    }

    // Dictionary to store OTP data for each user
    



public class UserService : IUserService
{
    private readonly ICustomerRepository _userRepository;

    public UserService(ICustomerRepository userRepository)
    {
        _userRepository = userRepository;
    }
    private static Dictionary<string, OTPData> otpStorage = new Dictionary<string, OTPData>();

    public bool ForgotPassword(string email)
    {
     var user = _userRepository.GetCustomerByEmail(email);
     if(user==null)
     {
      //_otp=null;
      return false; 
     }
       
        // Generate OTP
        var otp = GenerateOTP(); 
        otpStorage[email.ToLower()] = new OTPData { OTP = otp, ExpirationTime = DateTime.Now.AddMinutes(2) };
        //_otp = otp;
        // Send OTP via email
        SendOTPEmail(email, otp);

        return true;
    }

    public bool VerifyOTP(string email, string otp)
    {
         var user = _userRepository.GetCustomerByEmail(email.ToLower());
        if (user == null)
        return false;
        // You may want to implement some validation logic for the OTP here
        if (otpStorage.ContainsKey(email.ToLower()))
        {
            // Get the OTP data
            var otpData = otpStorage[email.ToLower()];

            // Check if the provided OTP matches and it hasn't expired
            if (otp == otpData.OTP && DateTime.Now < otpData.ExpirationTime)
            {
                // Remove the OTP data after successful verification
                otpStorage.Remove(email.ToLower());
                return true;
            }
        }
        return false;
    }

    public bool ResetPassword(string email, string newPassword, string ConfirmPassword)
    {
        // You can implement OTP verification logic here if required
        // For simplicity, I'm assuming the OTP is always valid
        var user = _userRepository.GetCustomerByEmail(email);
        if (user != null)
        {
            // Reset the password
            user.Password = newPassword; // Make sure to hash the password
            _userRepository.UpdateCustomer(user);
            SendResetEmail(email,user.FirstName);
            return true;
        }
        return false;
    }

    private string GenerateOTP()
{
    // Set the OTP length
    int otpLength = 6;

    // Generate a random OTP using a StringBuilder
    StringBuilder otp = new StringBuilder();
    Random random = new Random();
    for (int i = 0; i < otpLength; i++)
    {
        otp.Append(random.Next(0, 10)); // Append a random digit (0-9)
    }
    
    // Set the OTP expiration time to 2 minutes from now
    DateTime expirationTime = DateTime.Now.AddMinutes(2);
    
    // Store the OTP and its expiration time in some persistent storage (e.g., database)

    // For demonstration purposes, you can return the generated OTP
    return otp.ToString();
}


    private void SendOTPEmail(string email, string otp)
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
            Subject = "Password Reset OTP",
            Body = $"Your OTP for password reset is: {otp} and OTP will expire in 2 Minutes.",
            IsBodyHtml = true,
        };

        mailMessage.To.Add(email);

        smtpClient.Send(mailMessage);
    }
    private void SendResetEmail(string email,string name)
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
    Subject = "Password Reset Successful",
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
            <h1>Password Reset Successful</h1>
            <p>Dear " + name + @",</p>
            <p>Your password has been successfully reset.</p>
            <p>If you did not initiate this password reset, please contact us immediately.</p>
            <p>If you have any questions or need further assistance, feel free to reach out to us.</p>
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
