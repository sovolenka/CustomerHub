using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models;

public class Client
{
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
    [RegularExpression("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$")]
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
