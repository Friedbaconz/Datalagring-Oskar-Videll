

namespace DatalagringOskarVidell.Domain.Entities;

public class Kurs_Entity
{
    public string Kurskod { get; set; } = null!;
    public ICollection<Kurstillfalle_Entity> Kurstillfallen { get; set; } = [];

    public string Kursnamn { get; set; } = null!;

    public string Beskrivning { get; set; } = null!;
}
