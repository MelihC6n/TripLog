using MediatR;
using TS.Result;

namespace TripLogServer.Application.Features.User.UserCreate;
public sealed record UserCreateCommand(
    string UserName,
    string Email,
    string Password,
    string PasswordCheck,
    bool IsAuthor) : IRequest<Result<string>>;
