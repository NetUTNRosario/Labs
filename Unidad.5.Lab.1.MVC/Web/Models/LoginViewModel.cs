using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Web.Models
{
    public record LoginViewModel
    {
        public string Mail { get; init; }
        public string Clave { get; init; }
        public bool IsPersistent { get; init; }
    }

    public class LoginValidator : AbstractValidator<LoginViewModel>
    {
        public LoginValidator()
        {
            RuleFor(l => l.Mail).NotEmpty().EmailAddress();
            RuleFor(l => l.Password).NotEmpty();
        }
    }
}
