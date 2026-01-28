

namespace Datalagring_Oskar_Videll.Infrastructure.Entities;

public class StatusTypeEntity
{
    public int Id { get; set; }
    public string StatusName { get; set; } = null!;

    public virtual ICollection<DeltagareEntity> Deltagare { get; set; } = [];
}
