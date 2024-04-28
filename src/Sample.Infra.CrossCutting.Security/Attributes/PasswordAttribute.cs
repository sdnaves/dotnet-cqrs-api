using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Sample.Infra.CrossCutting.Security.Attributes
{
    public partial class PasswordAttribute : DataTypeAttribute
    {
        public PasswordAttribute() : base(DataType.Password)
        {
        }

        public override bool IsValid(object? value)
        {
            if (value is null)
                return true;

            var password = value.ToString()!;

            return PasswordRegex().IsMatch(password);
        }

        [GeneratedRegex(@"((?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[\W]).{8,64})")]
        private static partial Regex PasswordRegex();
    }
}
