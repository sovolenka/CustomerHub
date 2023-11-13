using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models;

public class Reminder
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(3000)]
    public string? Note { get; set; }
    public DateTime When { get; set; }

    [ForeignKey("ClientId")]
    public Client? Client { get; set; }

    [Required]
    [ForeignKey("UserId")]
    public User? User { get; set; }
}
