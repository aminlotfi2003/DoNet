namespace DoNet.WebFramework.Contracts.Identity;

public sealed record ChangePasswordRequest(string CurrentPassword, string NewPassword);
