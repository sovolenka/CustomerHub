using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Data.Models;

public class Reminder
{
    public Reminder() { }

    public Reminder(string note, string description, DateTime when, User user, Client? client)
    {
        Note = note;
        Description = description;
        When = when;
        Client = client;
        User = user;
    }

    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(300)]
    public string? Note { get; set; }

    [MaxLength(3000)]
    public string? Description { get; set; }

    public DateTime When { get; set; }

    [ForeignKey("ClientId")]
    public Client? Client { get; set; }

    [Required]
    [ForeignKey("UserId")]
    public User? User { get; set; }
}
