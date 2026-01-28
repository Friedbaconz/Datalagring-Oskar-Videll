
namespace Datalagring_Oskar_Videll.Infrastructure.Entities;

public class Kurstillfalle_Entity
{
    public int KurstillfalleId { get; set; }

    public string Kurskod { get; set; } = null!;
    public int MaxSeats { get; set; }

    public DateTime Startdatum { get; set; }
    public DateTime Slutdatum { get; set; }

    public int Ortid { get; set; }
}
