
namespace DatalagringOskarVidell.Domain.Entities;

public class KursRegi_Entity
{
    public int ID { get; set; }
    public int Antagen { get; set; }

    public ICollection<Kurstillfalle_Entity> Kurstillfallen = [];
    public ICollection<DeltagareEntity> DeltagareRegi = [];
    public DateTime RegiDatum { get; set; }
    public string status { get; set; } = null!;
}
