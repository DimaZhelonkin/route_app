using Ark.SharedLib.Common.CQS.Implementations;
using Ark.SharedLib.Common.Results;

namespace Ark.Rides.Application.Features.Passenger.Commands.SendFeedback;

public record SendFeedbackCommand : CommandResult
{
    public Guid RideId { get; set; }
    public Guid UserId { get; set; }

    public uint StarsCount { get; set; }
    public string FeedbackMessage { get; set; }
}

public class SendFeedbackCommandHandler : CommandHandler<SendFeedbackCommand>
{
    public override Task<Result> Handle(SendFeedbackCommand command, CancellationToken cancellationToken = default) =>
        throw new NotImplementedException();
}