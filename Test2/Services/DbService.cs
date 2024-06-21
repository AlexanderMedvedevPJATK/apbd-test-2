using ExampleTest2.Data;
using ExampleTest2.DTOs;
using ExampleTest2.Models;
using Microsoft.EntityFrameworkCore;

namespace ExampleTest2.Services;

public class DbService : IDbService
{
    private readonly ApplicationContext _context;
    
    public DbService(ApplicationContext context)
    {
        _context = context;
    }
    
    public async Task<bool> CharacterExists(int characterId)
    {
        return await _context.Characters.AnyAsync(c => c.Id == characterId);
    }
    
    public async Task<bool> ItemExists(int itemId)
    {
        return await _context.Items.AnyAsync(c => c.Id == itemId);
    }
    
    public async Task<Character?> GetBasicCharacterData(int characterId)
    {
        return await _context.Characters
            .Include(e => e.Backpacks)
                .ThenInclude(e => e.Item)
            .FirstOrDefaultAsync(e => e.Id == characterId);
    }
    
    public async Task<Character?> GetCharacterData(int characterId)
    {
        return await _context.Characters
            .Include(e => e.Backpacks)
                .ThenInclude(e => e.Item)
            .Include(e => e.CharacterTitles)
                .ThenInclude(e => e.Title)
            .FirstOrDefaultAsync(e => e.Id == characterId);
    }
    
    public async Task<Item?> GetItem(int itemId)
    {
        return await _context.Items.FirstOrDefaultAsync(e => e.Id == itemId);
    }
    
    public async Task AddNewItemForCharacter(Backpack backpack)
    {
        await _context.Backpacks.AddAsync(backpack);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateItemAmount(int characterId, int itemId, int additionalAmount)
    {
        var backpack = await _context.Backpacks.FirstOrDefaultAsync(e => e.CharacterId == characterId && e.ItemId == itemId);
        if (backpack == null)
            return;
        backpack.Amount += additionalAmount;
        
        await _context.SaveChangesAsync();
    }
}