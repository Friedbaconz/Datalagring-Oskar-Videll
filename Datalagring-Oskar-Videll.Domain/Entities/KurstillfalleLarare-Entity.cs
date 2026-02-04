
namespace DatalagringOskarVidell.Domain.Entities;

public class KurstillfalleLarare_Entity
{
    public Guid KursTillfallenId { get; set; }
    public ICollection<Kurstillfalle_Entity> Kurstillfallen = [];

    public string LarareEmail { get; set; } = null!;
    public ICollection<Larare_Entity> LarareRegi { get; set; } = [];

}
