

namespace Datalagring_Oskar_Videll.Domain.Entities;

public class Larare_Entity
{
    public string LarareEmail { get; set; } = null!;
    public ICollection<KurstillfalleLarare_Entity> KurstillfalleLarare = [];
    public string Fornamn { get; set; } = null!;
    public string Mellannamn { get; set; } = null!;
    public string Efternamn { get; set; } = null!;
    public string Kompentens { get; set; } = null!;
}
