
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Datalagring_Oskar_Videll.Infrastructure.Entities;

public class DeltagareEntity
{
    public string Email { get; set; } = null!;

    public string Fornamn { get; set; } = null!;
    public string? Mellannamn { get; set; }
    public string Efternamn { get; set; } = null!;
    public string? Telefonnummer { get; set; }

    public virtual ICollection<Role_Entity> Roles { get; set; } = [];
}
