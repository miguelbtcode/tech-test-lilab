namespace Application.Features.Entrances.Commands.RegisterEntrance;

using Abstractions.Messaging;
using MediatR;

public sealed class RegisterEntranceCommand: ICommand<bool>
{
    public DateTime? EntranceTime {get; set; }
    public int? CustomerId {get; set; }
}
    