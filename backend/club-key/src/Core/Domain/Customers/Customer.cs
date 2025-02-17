namespace Domain.Abstractions;

using Common;

public class Customer : Entity
{
    public string Name { get;  set; } = string.Empty;
    public string DocumentNumber { get; set; } = string.Empty;
    public CustomerType Type { get;  set; }
    public string PhoneNumber { get;  set; } = string.Empty;
    public DateOnly? BirthDate { get;  set; }
    public Gender Gender { get;  set; }
    public string Email { get;  set; } = string.Empty;
    public ICollection<Entrance> Entrances { get; set; } = new List<Entrance>();
    public ICollection<Exit> Exits { get; set; } = new List<Exit>();
    
    public Customer(){}

    private Customer(string name, string documentNumber, CustomerType type, string phoneNumber, DateOnly? birthDate, Gender gender, string email)
    {
        Name = name;
        DocumentNumber = documentNumber;
        Type = type;
        PhoneNumber = phoneNumber;
        BirthDate = birthDate;
        Gender = gender;
        Email = email;
    }
    
    public static Customer Create(string name, string documentNumber, CustomerType type, string phoneNumber, DateOnly? birthDate, Gender gender, string email)
    {
        return new Customer(name, documentNumber, type, phoneNumber, birthDate, gender, email);
    }
    
    private int CalculateAge()
    {
        if (BirthDate == null)
            return 0;

        var today = DateOnly.FromDateTime(DateTime.Today);
        var age = today.Year - BirthDate.Value.Year;

        if (today < BirthDate.Value.AddYears(age))
        {
            age--;
        }

        return age;
    }
    
    public void MapCustomer(
        int id,
        string name,
        string documentNumber,
        CustomerType type,
        string phoneNumber,
        DateOnly? birthDate,
        Gender gender,
        string email,
        bool isActive,
        DateTime createdDate,
        string? createdBy,
        DateTime? lastModifiedDate,
        string? lastModifiedBy)
    {
        Id = id;
        Name = name;
        DocumentNumber = documentNumber;
        Type = type;
        PhoneNumber = phoneNumber;
        BirthDate = birthDate;
        Gender = gender;
        Email = email;
        IsActive = isActive;
        CreatedDate = createdDate;
        CreatedBy = createdBy;
        LastModifiedDate = lastModifiedDate;
        LastModifiedBy = lastModifiedBy;
    }
}