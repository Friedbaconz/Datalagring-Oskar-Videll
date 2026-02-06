
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatalagringOskarVidell.Domain.Entities;

public class DeltagareEntity
{
    public Guid ID { get; set; }

    public string Fornamn { get; set; } = null!;
    public string? Mellannamn { get; set; }
    public string Efternamn { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Telefonnummer { get; set; }

    public ICollection<KursRegi_Entity> Kursregi = [];
}
