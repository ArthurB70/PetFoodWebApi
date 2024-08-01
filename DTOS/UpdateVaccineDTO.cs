namespace api;

public class UpdateVaccineDTO
{
    public int Id {get; set;}
    public string Name {get; set;} = String.Empty;
    public DateTime ApplicationDate {get; set;} = DateTime.MaxValue;
    public DateTime ExpirationDate {get; set;} = DateTime.MaxValue;

}
