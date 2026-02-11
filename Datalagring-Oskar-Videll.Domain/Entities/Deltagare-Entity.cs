
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatalagringOskarVidell.Domain.Entities;

public class DeltagareEntity
{
    public Guid ID { get; set; } = Guid.NewGuid();

    public string Fornamn { get; set; } = null!;
    public string? Mellannamn { get; set; }
    public string Efternamn { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Telefonnummer { get; set; }

    public ICollection<Kurstillfalle_Entity> KursRegiDeltagare = [];
}
