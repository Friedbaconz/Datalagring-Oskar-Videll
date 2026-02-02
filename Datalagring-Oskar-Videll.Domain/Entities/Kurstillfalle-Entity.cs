
namespace Datalagring_Oskar_Videll.Domain.Entities;

public class Kurstillfalle_Entity
{
    public Guid KursTillfallenId { get; set; }
    public ICollection<KursRegi_Entity> KursRegi = [];

    public string KursKod { get; set; } = null!;
    public Kurs_Entity Kurs { get; set; } = null!;

    public int MaxSeats { get; set; }
    public DateTime Startdatum { get; set; }
    public DateTime Slutdatum { get; set; }

    public Guid Ortid { get; set; }
    public Ort_Entity Ort = null!;

    public string LarareEmail { get; set; } = null!;
    public ICollection<KurstillfalleLarare_Entity> KursTillfallenLarare = [];
}
