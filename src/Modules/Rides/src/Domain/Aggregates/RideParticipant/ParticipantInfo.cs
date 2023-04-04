namespace Ark.Rides.Domain.Aggregates.RideParticipant;

public class ParticipantInfo
{
    public ParticipantInfo(string fio)
    {
        Fio = fio;
    }

    public string Fio { get; set; } // TODO maybe should store in account
}