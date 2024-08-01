namespace api;

public class AllergyDTO
{
    public int Id {get; set;}
    public string Name {get; set;} = String.Empty;
    public bool Vaccinated {get; set;} = false;
    public int PetId {get; set;}
}
