
namespace Datalagring_Oskar_Videll.Infrastructure.Entities;

public class KurstillfalleLarare_Entity
{
    public int KurstillfalleId { get; set; }
    public Kurstillfalle_Entity Kurstillfalle { get; set; } = null!;
    public string LarareEmail { get; set; } = null!;
    public Larare_Entity Larare { get; set; } = null!;
}
