using System.ComponentModel.DataAnnotations;

namespace Casgem_MicroServices.IdentityServer.Dto
{
    public class SignUpDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
