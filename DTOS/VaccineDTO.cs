namespace api;

public class VaccineDTO
{
    public int Id {get; set;}
    public string Name {get; set;} = String.Empty;
    public DateTime ApplicationDate {get; set;} = DateTime.MaxValue;
    public DateTime ExpirationDate {get; set;} = DateTime.MaxValue;
    public int PetId {get; set;}
}
