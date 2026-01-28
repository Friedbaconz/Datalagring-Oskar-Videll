
namespace Datalagring_Oskar_Videll.Infrastructure.Entities;

public class KursRegi_Entity
{
    public int KurstillfalleId { get; set; }
    public Kurstillfalle_Entity Kurstillfalle { get; set; } = null!;
    public string DeltagareEmail { get; set; } = null!;
    public DeltagareEntity Deltagare { get; set; } = null!;
    public DateTime RegiDatum { get; set; }
    public string status { get; set; } = null!;
}
