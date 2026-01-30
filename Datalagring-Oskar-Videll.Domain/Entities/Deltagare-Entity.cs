
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Datalagring_Oskar_Videll.Domain.Entities;

public class DeltagareEntity
{
    public string Email { get; set; } = null!;

    public string Fornamn { get; set; } = null!;
    public string? Mellannamn { get; set; }
    public string Efternamn { get; set; } = null!;
    public string? Telefonnummer { get; set; }

    public virtual ICollection<KursRegi_Entity> Kurstillfallen { get; set; } = [];

    public int StatusTypeId { get; set; }
    public StatusTypeEntity StatusType { get; set; } = null!;
}
