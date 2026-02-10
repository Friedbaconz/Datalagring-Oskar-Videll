
namespace DatalagringOskarVidell.Domain.Entities;

public class Kurstillfalle_Entity
{
    public Guid ID { get; set; }

    public string KursKodID { get; set; } = null!;
    public Kurs_Entity Kurs { get; set; } = null!;

    public int MaxSeats { get; set; }
    public DateTime Startdatum { get; set; }
    public DateTime Slutdatum { get; set; }

    public int Ortid { get; set; }

    public Ort_Entity Ort = null!;


    public ICollection<DeltagareEntity> KursRegi = [];

    public ICollection<Larare_Entity> KursTillfallenLarare = [];
}
