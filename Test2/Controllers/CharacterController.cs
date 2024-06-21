using System.Transactions;
using ExampleTest2.DTOs;
using ExampleTest2.Models;
using ExampleTest2.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace ExampleTest2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private readonly IDbService _dbService;

        public CharacterController(IDbService dbService)
        {
            _dbService = dbService;
        }

        [HttpGet("{characterId:int}")]
        public async Task<IActionResult> GetCharacter(int characterId)
        {
            if (characterId <= 0)
                return BadRequest("Character ID must be greater than 0");

            var characterData = await _dbService.GetCharacterData(characterId);
            if (characterData is null)
                return NotFound($"Character with given ID - {characterId} doesn't exist");
            
            
            var characterDTO = new GetCharacterDTO()
            {
                FirstName = characterData.FirstName,
                LastName = characterData.LastName,
                CurrentWeight = characterData.CurrentWeight,
                MaxWeight = characterData.MaxWeight,
                BackpackItems = characterData.Backpacks.Select(e => new BackpackDTO()
                {
                    ItemName = e.Item.Name,
                    ItemWeight = e.Item.Weight,
                    Amount = e.Amount
                }).ToList(), 
                Titles = characterData.CharacterTitles.Select(e => new TitleDTO()
                {
                    Title = e.Title.Name,
                    AcquiredAt = e.AcquiredAt
                }).ToList()
            };

            return Ok(characterDTO);
        }
        
        [HttpPost("{characterId:int}/backpacks")]
        public async Task<IActionResult> AddItemToBackpack(int characterId, List<AddItemDTO> itemDtos)
        {
            if (characterId <= 0)
                return BadRequest("Character ID must be greater than 0");
            
            var character = await _dbService.GetBasicCharacterData(characterId);
            if (character is null)
                return NotFound($"Character with given ID - {characterId} doesn't exist");

            var itemAddedDtoList = new List<ItemAddedDTO>();
            
            foreach (var itemDto in itemDtos)
            {
                var item = await _dbService.GetItem(itemDto.ItemId);
                if (item is null)
                    return NotFound($"Item with given ID - {itemDto.ItemId} doesn't exist");
                
                if (character.CurrentWeight + itemDto.Amount * item.Weight > character.MaxWeight)
                    return BadRequest("Character can't carry that much weight");
                
                var hasSuchItems = character.Backpacks.Select(e => e.ItemId == item.Id);
                if (hasSuchItems.IsNullOrEmpty())
                {
                    Console.WriteLine("----------- Updating item");
                    await _dbService.UpdateItemAmount(characterId, itemDto.ItemId, itemDto.Amount);
                }
                else
                {
                    
                    Console.WriteLine("----------- Adding new item");
                    var backpack = new Backpack()
                    {
                        CharacterId = characterId,
                        ItemId = item.Id,
                        Amount = itemDto.Amount
                    };


                    using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        character.CurrentWeight += itemDto.Amount * item.Weight;
                        await _dbService.AddNewItemForCharacter(backpack);
                        scope.Complete();
                        Console.WriteLine("----------- New item added");
                    }

                    var itemAddedDto = new ItemAddedDTO()
                    {
                        Amount = itemDto.Amount,
                        ItemId = item.Id,
                        CharacterId = characterId
                    };
                    
                    itemAddedDtoList.Add(itemAddedDto);
                }
                
            }
            
            return Created("api/backpacks", itemAddedDtoList);
        }
    }
    
    
}
