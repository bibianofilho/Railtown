using FluentValidation;
using Microsoft.Extensions.Logging;
using Railtown.Interview.Api.Models;

namespace Railtown.Interview.Api.Validators
{
    public class UserValidator : UserAbstractValidator<User>
    {
        public UserValidator(ILogger<User> logger) : base(logger)
        {
            RuleFor(i => i.Name).NotEmpty();
            RuleFor(i => i.Address).NotEmpty();
        }        
    }
}
