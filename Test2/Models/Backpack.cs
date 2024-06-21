using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ExampleTest2.Models;

[PrimaryKey(nameof(CharacterId), nameof(ItemId))]
public class Backpack
{
    
    public int CharacterId { get; set; }
    
    [ForeignKey(nameof(CharacterId))]
    public Character Character { get; set; } = null!;
    
    public int ItemId { get; set; }
    
    [ForeignKey(nameof(ItemId))]
    public Item Item { get; set; } = null!;
    
    public int Amount { get; set; }
}