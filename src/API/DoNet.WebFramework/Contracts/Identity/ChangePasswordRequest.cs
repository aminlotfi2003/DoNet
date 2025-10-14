namespace DoNet.WebFramework.Contracts.Identity;

public sealed record ChangePasswordAfter90DaysRequest(string CurrentPassword, string NewPassword);
