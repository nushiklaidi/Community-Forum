
using System.ComponentModel.DataAnnotations;

namespace CleanArchitecture.Application.ViewModels
{
    public class AuthViewModel
    {
        public string Username { get; set; }        
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
