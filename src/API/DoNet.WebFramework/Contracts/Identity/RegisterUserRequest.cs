using DoNet.Domain.Identity;

namespace DoNet.WebFramework.Contracts.Identity;

public sealed record RegisterUserRequest(
    string Email,
    string Password,
    string ConfirmPassword,
    string FirstName,
    string LastName,
    Gender Gender,
    DateTimeOffset BirthDate
);
