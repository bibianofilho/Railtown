using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace Railtown.Interview.Api.Validators
{
    public class UserAbstractValidator<T> : AbstractValidator<T>
    {
        private readonly ILogger<T> _logger;
        public UserAbstractValidator(ILogger<T> logger) => _logger = logger;

        public override ValidationResult Validate(ValidationContext<T> context)
        {
            var validationResults = base.Validate(context);

            if (!validationResults.IsValid)
            {
                _logger.LogInformation("Validation Failed on {Model} with message(s): {Errors}", typeof(T),
                    validationResults.ToString(" - "));
            }

            return validationResults;
        }
    }
}
