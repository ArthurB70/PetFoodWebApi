namespace api;

public class Medicine
{
    public int Id {get; set;}
    public string Name {get; set;} = String.Empty;
    public decimal Dousage {get; set;}
    public string Type {get; set;} = String.Empty;
    public string MedicineSchedule {get; set;} = String.Empty;
    public int PetId {get; set;}
    //public Pet Pet {get; set;} = new Pet();
}
