using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models;

public class Client
{
    public Client() { }

    public Client
    (
        string firstName,
        string secondName,
        string thirdName,
        string phoneNumber,
        string email,
        string address,
        string factory,
        DateOnly dateAdded,
        ClientStatus status
    )
    {
        FirstName = firstName;
        SecondName = secondName;
        ThirdName = thirdName;
        PhoneNumber = phoneNumber;
        Email = email;
        Address = address;
        Factory = factory;
        DateAdded = dateAdded;
        Status = status;
    }

    [Key]
    public int Id { get; set; }

    [Required]
    [MinLength(2)]
    [MaxLength(100)]
    public string? FirstName { get; set; }

    [Required]
    [MinLength(2)]
    [MaxLength(100)]
    public string? SecondName { get; set; }

    public string? ThirdName { get; set; }

    [Required]
    [RegularExpression("^[0-9]{10,16}$")]
    public string? PhoneNumber { get; set; }

    [Required]
    [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$")]
    public string? Email { get; set; }

    [Required]
    public string? Address { get; set; }

    [Required]
    public string? Factory { get; set; }

    [Required]
    public DateOnly DateAdded { get; set; }

    [Required]
    public ClientStatus Status { get; set; }

    public List<Product> Products { get; set; } = new List<Product>();

    public List<Reminder> Reminders { get; set; } = new List<Reminder>();

    [Required]
    [ForeignKey("UserId")]
    public User? User { get; set; }
}

public enum ClientStatus
{
    Active, Inactive
}
