using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models;

public class Product
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MinLength(2)]
    [MaxLength(300)]
    public string? Name { get; set; }

    public int Price { get; set; }

    [Required]
    [ForeignKey("CharacteristicId")]
    public Characteristic? Characteristic { get; set; }

    [Required]
    [ForeignKey("UserId")]
    public User? User { get; set; }
}
