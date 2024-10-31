using MediatR;
using Microsoft.AspNetCore.Identity;
using TripLogServer.Domain.Entities;
using TS.Result;

namespace TripLogServer.Application.Features.User.UserCreate;

internal sealed class UserCreateCommandHandler(
    UserManager<AppUser> userManager) : IRequestHandler<UserCreateCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UserCreateCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.UserName) ||
            string.IsNullOrEmpty(request.Email) ||
            string.IsNullOrEmpty(request.Password) ||
            string.IsNullOrEmpty(request.PasswordCheck))
        {
            return Result<string>.Failure("Lütfen tüm alanları doldurunuz!");
        }

        if (request.Password != request.PasswordCheck)
        {
            return Result<string>.Failure("Şifre ve şifre tekrarı eşleşmiyor. Lütfen aynı şifreyi giriniz!");
        }

        if (userManager.Users.Any(x => x.UserName == request.UserName))
        {
            return Result<string>.Failure("Bu kullanıcı adı mevcut, lütfen farklı bir kullanıcı adı deneyiniz!");
        }

        if (userManager.Users.Any(x => x.Email == request.Email))
        {
            return Result<string>.Failure("Bu email adresi zaten kayıtlı, farklı bir mail adresi deneyiniz!");
        }

        AppUser user = new()
        {
            UserName = request.UserName,
            Email = request.Email,
            IsAuthor = request.IsAuthor
        };

        IdentityResult result = await userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            return Result<string>.Failure("Kullanıcı kaydı oluşturulurken bir hata ile karşılaşıldı. Lütfen tüm alanları kontorl ediniz! Hata Kodu : " + string.Join("\n ", result.Errors.Select(x => x.Description)));
        }

        return "Kullanıcı kaydı başarıyle tamamlandı.\n Aramıza hoş geldin " + user.UserName + "!";
    }
}