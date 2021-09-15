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
        [Display(Name = "Confirmar Clave")]
        public string ConfirmarClave { get; init; }
        public bool IsPersistent { get; init; }
    }

    public class RegisterValidator: AbstractValidator<RegisterViewModel>
    {
        public RegisterValidator()
        {
            RuleFor(rvm => rvm.Nombre).Length(min: 3, max: 30);
            RuleFor(rvm => rvm.Mail).NotEmpty().EmailAddress();
            RuleFor(rvm => rvm.Clave).NotEmpty().MinimumLength(6)
                // No se utiliza .Matches(@"\w*[A-Z]+\w*") ya que esto solo reconoce caracteres ASCII,
                // es decir da errores de validacion al usar caracteres Unicode como 'Ñ' o 'ñ'
                .Must(p => p.Any(Char.IsUpper)).WithMessage("La contraseña debe tener al menos un caracter en mayuscula")
                .Must(p => p.Any(Char.IsNumber)).WithMessage("La contraseña debe tener al menos un numero");
            RuleFor(rvm => rvm.ConfirmarClave).Equal(rvm => rvm.Clave);
        }
    }
}
