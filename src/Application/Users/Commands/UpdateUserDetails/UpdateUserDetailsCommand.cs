using MediatR;

namespace Application.User.Commands.UpdateUserDetails;

public class UpdateUserDetailsCommand : IRequest
{
    public DateOnly DateOfBirth { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
}
