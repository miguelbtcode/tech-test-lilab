namespace Domain.Abstractions;

using Common;

public class Exit : Entity
{
    public DateTime ExitTime { get; set; }
    public bool IsLastStatus { get; set; }

    // Clave Foránea
    public int CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;
}