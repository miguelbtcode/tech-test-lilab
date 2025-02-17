namespace Domain.Abstractions;

using Common;

public class Entrance : Entity
{
    public DateTime EntranceTime { get; set; }
    public bool IsLastStatus { get; set; }
    // Clave Foránea
    public int CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;
}