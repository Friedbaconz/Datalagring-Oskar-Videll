
namespace Datalagring_Oskar_Videll.Domain.Entities;

public class Kurstillfalle_Entity
{
    public int KurstillfalleId { get; set; }

    public string KursKod { get; set; } = null!;
    public Kurs_Entity Kurs { get; set; } = null!;

    public int MaxSeats { get; set; }
    public DateTime Startdatum { get; set; }
    public DateTime Slutdatum { get; set; }

    public int Ortid { get; set; }
    public Ort_Entity Ort { get; set; } = null!;

    public ICollection<KurstillfalleLarare_Entity> KurstillfalleLarare { get; set; } = new List<KurstillfalleLarare_Entity>();

    public ICollection<KursRegi_Entity> KursRegi { get; set; } = new List<KursRegi_Entity>();
}
