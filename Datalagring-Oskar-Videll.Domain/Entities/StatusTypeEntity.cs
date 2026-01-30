

namespace Datalagring_Oskar_Videll.Domain.Entities;

public class StatusTypeEntity
{
    public Guid Id { get; set; }
    public string StatusName { get; set; } = null!;

    public virtual ICollection<DeltagareEntity> Deltagare { get; set; } = [];
}
