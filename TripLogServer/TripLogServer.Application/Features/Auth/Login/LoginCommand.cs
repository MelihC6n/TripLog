using MediatR;
using Microsoft.AspNetCore.Identity;
using TripLogServer.Application.Services;
using TripLogServer.Domain.Entities;
using TS.Result;

namespace TripLogServer.Application.Features.Auth.Login;
public sealed record LoginCommand(
    string UserNameOrEmail,
    string Password) : IRequest<Result<LoginCommandResponse>>;

public sealed record LoginCommandResponse(
    string Token);

internal sealed class LoginCommandHandler(
    UserManager<AppUser> userManager,
    IJwtProvider jwtProvider) : IRequestHandler<LoginCommand, Result<LoginCommandResponse>>
{
    public async Task<Result<LoginCommandResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        AppUser? user = await userManager.FindByNameAsync(request.UserNameOrEmail) ?? await userManager.FindByEmailAsync(request.UserNameOrEmail);

        if (user == null)
        {
            return Result<LoginCommandResponse>.Failure("Kullanıcı bulunamadı! Bilgilerinizi kontrol ediniz!");
        }

        bool isPasswordCorrect = await userManager.CheckPasswordAsync(user, request.Password);
        if (!isPasswordCorrect)
        {
            return Result<LoginCommandResponse>.Failure("Kullanıcı şifresi yanlış");
        }

        string token = jwtProvider.CreateToken(user);
        LoginCommandResponse response = new(token);
        return Result<LoginCommandResponse>.Succeed(response);
    }
}
