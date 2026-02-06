

namespace DatalagringOskarVidell.Domain.Entities;

public class Ort_Entity
{
    public int OrtId { get; set; }
    public string OrtNamn { get; set; } = null!;
    public ICollection<Kurstillfalle_Entity> Kurstillfallen = null!;
}
