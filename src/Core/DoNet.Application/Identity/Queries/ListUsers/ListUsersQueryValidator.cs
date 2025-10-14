using FluentValidation;

namespace DoNet.Application.Identity.Queries.ListUsers;

public sealed class ListUsersQueryValidator : AbstractValidator<ListUsersQuery>
{
    public ListUsersQueryValidator()
    {
        RuleFor(x => x.IncludeInactive)
            .Must(_ => true);
    }
}
