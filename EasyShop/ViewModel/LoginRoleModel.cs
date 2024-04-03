using System.ComponentModel.DataAnnotations;

namespace EasyShop.ViewModel
{
    public class LoginRoleModel
    {
        public string Email { get; set; }

        
        public string Password { get; set; }

        public string Role { get; set; }
    }
}
