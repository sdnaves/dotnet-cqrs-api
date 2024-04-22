using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Sample.Application.ViewModels
{
    public class CustomerViewModel(string id, string name, string email, DateTime birthDate)
    {
        [Key]
        public string Id { get; set; } = id;

        [Required(ErrorMessage = "The Name is Required")]
        [MinLength(2)]
        [MaxLength(100)]
        [DisplayName("Name")]
        public string Name { get; set; } = name;

        [Required(ErrorMessage = "The E-mail is Required")]
        [EmailAddress]
        [DisplayName("E-mail")]
        public string Email { get; set; } = email;

        [Required(ErrorMessage = "The BirthDate is Required")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        [DisplayName("Birth Date")]
        public DateTime BirthDate { get; set; } = birthDate;
    }
}
