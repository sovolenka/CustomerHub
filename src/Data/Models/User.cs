using System.ComponentModel.DataAnnotations;

namespace Data.Models;

public class User
{
    public User() { }

    public User(string email, byte[] passwordHash)
    {
        Email = email;
        PasswordHash = passwordHash;
    }

    [Key]
    public int Id { get; set; }

    [Required]
    [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$")]
    public string? Email { get; set; }

    [Required]
    public byte[]? PasswordHash { get; set; }

    public List<Product> Products { get; set; } = new List<Product>();

    public List<Client> Clients { get; set; } = new List<Client>();

    public List<Reminder> Reminders { get; set; } = new List<Reminder>();
}
