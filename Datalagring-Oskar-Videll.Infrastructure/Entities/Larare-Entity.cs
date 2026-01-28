

namespace Datalagring_Oskar_Videll.Infrastructure.Entities;

public class Larare_Entity
{
    public string LarareEmail { get; set; } = null!;
    public string fornamn { get; set; } = null!;
    public string mellannamn { get; set; } = null!;
    public string efternamn { get; set; } = null!;
    public string kompentens { get; set; } = null!;
    public virtual ICollection<KurstillfalleLarare_Entity> KurstillfalleLarare { get; set; } = [];
}
