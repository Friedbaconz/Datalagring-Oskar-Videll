

namespace Datalagring_Oskar_Videll.Domain.Entities;

public class Ort_Entity
{
    public Guid OrtId { get; set; }
    public string OrtNamn { get; set; } = null!;
    public virtual ICollection<Kurstillfalle_Entity> Kurstillfallen { get; set; } = [];
}
