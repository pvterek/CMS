using System.ComponentModel.DataAnnotations;

namespace CMS.Models.CustomValidationAttribute
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class DateTimeRangeAttribute(DateTimeRangeMode mode) : ValidationAttribute
    {
        private DateTime _minDate;
        private DateTime _maxDate;

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            SetDateRange();

            if (value is DateTime dateTime)
            {
                if (dateTime < _minDate || dateTime > _maxDate)
                {
                    return new ValidationResult(
                        ErrorMessage ?? $"The date must be between {_minDate.ToShortDateString()} and {_maxDate.ToShortDateString()}.");
                }
            }

            return ValidationResult.Success;
        }

        private void SetDateRange()
        {
            switch (mode)
            {
                case DateTimeRangeMode.Birthday:
                    _minDate = DateTime.Today.AddYears(-100);
                    _maxDate = DateTime.Today;
                    break;
                case DateTimeRangeMode.Visit:
                    _minDate = DateTime.Today;
                    _maxDate = DateTime.Today.AddYears(100);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode),
                        "Invalid DateTimeRangeMode.");
            }
        }
    }
}