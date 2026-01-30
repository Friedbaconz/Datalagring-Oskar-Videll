
namespace Datalagring_Oskar_Videll.Domain.Entities;

public class KursRegi_Entity
{
    public int KursTillfallenId { get; set; }
    public Kurstillfalle_Entity Kurstillfalle { get; set; } = null!;
    public DeltagareEntity DeltagareEmail { get; set; } = null!;
    public DateTime RegiDatum { get; set; }
    public string status { get; set; } = null!;
}
