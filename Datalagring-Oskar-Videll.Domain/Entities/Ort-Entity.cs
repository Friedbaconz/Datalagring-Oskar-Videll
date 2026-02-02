

namespace Datalagring_Oskar_Videll.Domain.Entities;

public class Ort_Entity
{
    public Guid OrtId { get; set; }
    public string OrtNamn { get; set; } = null!;
    public Kurstillfalle_Entity Kurstillfallen = null!;
}
