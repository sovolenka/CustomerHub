using System.ComponentModel.DataAnnotations;

namespace Data.Models;

public class Characteristic
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string? ProductType { get; set; }

    [Required]
    public string? Category { get; set; }

    [Required]
    public string? Description { get; set; }

    [Required]
    public string? Manufacturer { get; set; }

    [Required]
    public string? Country { get; set;}

    public DateOnly ManufactureDate { get; set; }

    public ProductStatus Status { get; set; }

    [Required]
    public Product? Product { get; set; }
}

public enum ProductStatus
{
    New, Used, OpenBox, Refurbished
}
