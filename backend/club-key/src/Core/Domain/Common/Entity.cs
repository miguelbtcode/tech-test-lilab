namespace Domain.Common;

public abstract class Entity
{
    public int Id { get; set; }

    public DateTime CreatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? LastModifiedDate { get; set; }

    public string? LastModifiedBy { get; set;}
    public bool IsActive { get; set; }
}