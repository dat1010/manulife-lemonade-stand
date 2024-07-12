namespace LemonadeStandApi.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Product
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(255)]
    public string Name { get; set; }

    [Required]
    [StringLength(255)]
    public string Type { get; set; }

    [Required]
    [StringLength(50)]
    public string Size { get; set; }

    [Required]
    public decimal Price { get; set; } 

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
