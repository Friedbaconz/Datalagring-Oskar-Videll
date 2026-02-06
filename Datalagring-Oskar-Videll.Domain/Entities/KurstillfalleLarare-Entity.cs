
namespace DatalagringOskarVidell.Domain.Entities;

public class KurstillfalleLarare_Entity
{
    public int ID { get; set; }

    public string Larare { get; set; } = null!;

    public ICollection<Kurstillfalle_Entity> Kurstillfallen = [];
    public ICollection<Larare_Entity> LarareRegi = [];

}
