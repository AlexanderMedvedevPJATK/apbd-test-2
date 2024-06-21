using ExampleTest2.Models;

namespace ExampleTest2.Services;

public interface IDbService
{
    
    Task<Character?> GetBasicCharacterData(int characterId);
    Task<Character?> GetCharacterData(int characterId);
    Task<Item?> GetItem(int itemId);
    Task AddNewItemForCharacter(Backpack backpack);
    Task UpdateItemAmount(int characterId, int itemId, int additionalAmount);
}