using System.ComponentModel.DataAnnotations;

namespace Data.Models;

public class User
{
    [Key]
    public int Id { get; set; }

    [Required]
    [RegularExpression("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$")]
    public string? Email { get; set; }

    [Required]
    [RegularExpression("^(?=.*[A-Za-z])(?=.*\\d)(?=.*[@$!%*#?&])[A-Za-z\\d@$!%*#?&]{8,}$")]
    public string? Password { get; set; }

    public List<Product> Products { get; set; } = new List<Product>();

    public List<Client> Clients { get; set; } = new List<Client>();

    public List<Reminder> Reminders { get; set; } = new List<Reminder>();
}
