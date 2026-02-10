
namespace DatalagringOskarVidell.Domain.Entities;

public class KursRegi_Entity
{
    public Guid ID { get; set; }
    public Guid Antagen { get; set; }
    public Kurstillfalle_Entity Kurstillfallen { get; set; } = null!;
    public DeltagareEntity DeltagareRegi { get; set; } = null!;
    public DateTime RegiDatum { get; set; }
    public string status { get; set; } = null!;

}
