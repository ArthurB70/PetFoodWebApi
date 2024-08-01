namespace api;

public class UpdatePetDTO
{
    public string Specie {get; set;} = String.Empty;
    public string Name {get; set;} = String.Empty;
    public string Breed {get; set;} = String.Empty;
    public DateTime BirthDate {get; set;} = DateTime.MaxValue;
    public string Gender {get; set;} = String.Empty;
    public string Size {get; set;} = String.Empty;

}
