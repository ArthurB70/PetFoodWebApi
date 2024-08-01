namespace api;

public class CreateAllergyDTO
{
    public string Name {get; set;} = String.Empty;
    public bool Vaccinated {get; set;} = false;
}
