
namespace DatalagringOskarVidell.Domain.Entities;

public class KursRegi_Entity
{
    public Guid KursRegiId { get; set; }
    public ICollection<Kurstillfalle_Entity> Kurstillfallen = [];

    public Guid DeltagareEmail { get; set; }
    public ICollection<DeltagareEntity> DeltagareRegi = [];

    public DateTime RegiDatum { get; set; }
    public string status { get; set; } = null!;
}
