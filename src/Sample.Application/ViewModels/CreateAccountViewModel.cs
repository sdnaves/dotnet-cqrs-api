using Sample.Infra.CrossCutting.Security.Attributes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Sample.Application.ViewModels
{
    public class CreateAccountViewModel
    {
        [MinLength(2)]
        [MaxLength(100)]
        public required string Name { get; set; }

        [EmailAddress]
        [DisplayName("E-mail")]
        public required string Email { get; set; }

        [Password]
        public required string Password { get; set; }

        [Password]
        [Compare(nameof(Password))]
        public required string ConfirmPassword { get; set; }
    }
}
