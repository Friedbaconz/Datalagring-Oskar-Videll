

namespace DatalagringOskarVidell.Domain.Entities;

public class Larare_Entity
{
    public string Email { get; set; } = null!;

    public ICollection<Kurstillfalle_Entity> KurstillfalleLarare = [];
    public string Fornamn { get; set; } = null!;
    public string Mellannamn { get; set; } = null!;
    public string Efternamn { get; set; } = null!;
    public string Kompentens { get; set; } = null!;
}
