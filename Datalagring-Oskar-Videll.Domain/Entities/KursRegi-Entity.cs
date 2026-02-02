
namespace Datalagring_Oskar_Videll.Domain.Entities;

public class KursRegi_Entity
{
    public Guid KursRegiId { get; set; }
    public ICollection<Kurstillfalle_Entity> Kurstillfallen = [];

    public string DeltagareEmail { get; set; } = null!;
    public ICollection<DeltagareEntity> DeltagareRegi = [];

    public DateTime RegiDatum { get; set; }
    public string status { get; set; } = null!;
}
