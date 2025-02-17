namespace Application.Features.Exits.Commands.RegisterExit;

using Abstractions.Messaging;
using MediatR;

public sealed record RegisterExitCommand : ICommand<bool>
{
    public DateTime? ExitTime { get; set; }
    public int? CustomerId { get; set; }
}
