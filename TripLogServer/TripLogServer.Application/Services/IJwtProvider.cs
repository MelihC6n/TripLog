using TripLogServer.Domain.Entities;

namespace TripLogServer.Application.Services;
public interface IJwtProvider
{
    string CreateToken(AppUser user);
}
