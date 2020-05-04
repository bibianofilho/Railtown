using FluentValidation;
using Microsoft.Extensions.Logging;
using Railtown.Interview.Api.Models;

namespace Railtown.Interview.Api.Validators
{
    public class GeoLocationValidator : UserAbstractValidator<GeoLocation>
    {
        public GeoLocationValidator(ILogger<GeoLocation> logger) : base(logger)
        {
            RuleFor(i => i.Lat).InclusiveBetween(-90, 90);
            RuleFor(i => i.Lng).InclusiveBetween(-180,180);
        }

        private bool BeGreaterThanZero(decimal value) => value > 0;
    }
}
