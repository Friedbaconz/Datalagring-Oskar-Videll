
namespace DatalagringOskarVidell.Domain.Entities;

public class KurstillfalleLarare_Entity
{
    public Guid ID { get; set; }

    public string Larare { get; set; } = null!;

    public Kurstillfalle_Entity Kurstillfallen { get; set; } = null!;
    public Larare_Entity LarareRegi { get; set; } = null!;

}
