

namespace Datalagring_Oskar_Videll.Domain.Entities;

public class Role_Entity
{
    public string RoleEmail { get; set; } = null!;
    public string RoleName { get; set; } = null!;

    public virtual ICollection<DeltagareEntity> Deltagare { get; set; } = [];
}
