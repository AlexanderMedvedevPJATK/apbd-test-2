namespace ExampleTest2.DTOs;

public class GetCharacterDTO
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public int CurrentWeight { get; set; }
    public int MaxWeight { get; set; }
    public ICollection<BackpackDTO> BackpackItems { get; set; } = new List<BackpackDTO>();
    public ICollection<TitleDTO> Titles { get; set; } = new List<TitleDTO>();
}