using System.ComponentModel.DataAnnotations;

namespace ExampleTest2.Models;

public class Item
{
    [Key]
    public int Id { get; set; }

    [MaxLength(100)]
    [Required]
    public string Name { get; set; } = string.Empty;
    
    public int Weight { get; set; }
}