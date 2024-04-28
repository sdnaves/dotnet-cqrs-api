using Sample.Infra.CrossCutting.Attributes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Sample.Application.ViewModels
{
    public class AccountViewModel
    {
        [Key]
        public string Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }
    }
}
