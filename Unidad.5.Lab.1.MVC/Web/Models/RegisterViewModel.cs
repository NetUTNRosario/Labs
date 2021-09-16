using FluentValidation;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Web.Models
{
    public record RegisterViewModel
    {
        public string Nombre { get; set; }
        public string Mail { get; init; }
        public string Clave { get; init; }
        public string ConfirmarClave { get; init; }
        public bool IsPersistent { get; init; }
    }

    public class RegisterValidator: AbstractValidator<RegisterViewModel>
    {
        public RegisterValidator()
        {
        }
    }
}
