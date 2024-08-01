namespace api;

public class Allergy
{
    public int Id {get; set;}
    public string Name {get; set;} = String.Empty;
    public bool Vaccinated {get; set;} = false;
    public int PetId {get; set;}
    //public Pet Pet {get; set;} = new Pet();
}
