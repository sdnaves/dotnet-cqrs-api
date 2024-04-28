using Sample.Infra.CrossCutting.Security.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Sample.Infra.CrossCutting.Security.Models
{
    public class AuthUserRequest
    {
        [EmailAddress]
        [Required(AllowEmptyStrings = false)]
        public required string Email { get; set; }

        [Password]
        [Required(AllowEmptyStrings = false)]
        public required string Password { get; set; }
    }
}
