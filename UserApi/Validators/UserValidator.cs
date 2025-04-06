using FluentValidation;
using UserApi.Models.ViewModels;

namespace UserApi.Validators
{
    public class UserValidator : AbstractValidator<DtoUserUI>
    {
        public UserValidator()
        {
            RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("Ad alanı gereklidir.")
            .Matches(@"^[A-Za-z]+$").WithMessage("Ad sadece harflerden oluşabilir.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Soyad alanı gereklidir.")
                .Matches(@"^[A-Za-z]+$").WithMessage("Soyad sadece harflerden oluşabilir.");

            RuleFor(x => x.NickName)
                .Matches(@"^[A-Za-z0-9_]+$").WithMessage("Takma ad sadece harf, rakam ve alt çizgi (_) içerebilir.")
                .When(x => !string.IsNullOrEmpty(x.NickName));

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("E-posta alanı gereklidir.")
                .Matches(@"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$").WithMessage("Geçersiz e-posta formatı.");

            RuleFor(x => x.HashPassword)
                .NotEmpty().WithMessage("Şifre alanı gereklidir.").
                Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).+$")
                .MinimumLength(8).WithMessage("Şifre en az 8 karakter olmalıdır.");
        }
    }
}
