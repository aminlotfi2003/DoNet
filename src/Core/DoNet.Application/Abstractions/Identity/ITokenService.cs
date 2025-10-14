using DoNet.Application.Identity.Models;
using DoNet.Domain.Identity;

namespace DoNet.Application.Abstractions.Identity;

public interface ITokenService
{
    TokenPair GenerateTokenPair(ApplicationUser user);
    string ComputeHash(string value);
}
