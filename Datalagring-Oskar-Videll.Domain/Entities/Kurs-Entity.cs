

namespace Datalagring_Oskar_Videll.Domain.Entities;

public class Kurs_Entity
{
    public string Kurskod { get; set; } = null!;
    public virtual ICollection<KursRegi_Entity> Kurstillfallen { get; set; } = [];

    public string Kursnamn { get; set; } = null!;

    public string Beskrivning { get; set; } = null!;
}
