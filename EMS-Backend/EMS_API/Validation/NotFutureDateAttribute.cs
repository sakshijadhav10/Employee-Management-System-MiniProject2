using System.ComponentModel.DataAnnotations;

namespace EMS.API.Validation
{
    public class NotFutureDateAttribute: ValidationAttribute
    {

        public override bool IsValid(object value)
        {
            if (value == null) return true;

            var date = (DateTime)value;

            return date <= DateTime.Today;
        }

        public override string FormatErrorMessage(string name)
        {
            return $"{name} cannot be greater than today";
        }
    }
}
