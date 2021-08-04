using ECommerce.Basket.Data.Entities;
using FluentValidation;
namespace ECommerce.Basket.Business.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(p => p.Username).NotNull().NotEmpty().WithMessage("Kullanıcı adı boş geçilemez.");
            RuleFor(p => p.Password).NotNull().NotEmpty().WithMessage("Şifre boş geçilemez");
        }
    }
}
