using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Data.Models;

public class Characteristic
{
    public Characteristic() { }

    public Characteristic
    (
        string productType,
        string category,
        string description,
        string manufacturer,
        string country,
        DateOnly manufactureDate,
        ProductStatus productStatus
    )
    {
        ProductType = productType;
        Category = category;
        Description = description;
        Manufacturer = manufacturer;
        Country = country;
        ManufactureDate = manufactureDate;
        ProductStatus = productStatus;
    }

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
    public string? Country { get; set; }

    public DateOnly ManufactureDate { get; set; }

    public ProductStatus ProductStatus { get; set; }

    [Required]
    [ForeignKey("ProductId")]
    public Product? Product { get; set; }
}

public enum ProductStatus
{
    New, Used, OpenBox, Refurbished
}
