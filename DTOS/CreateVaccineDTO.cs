namespace api;

public class CreateVaccineDTO
{
    public string Name {get; set;} = String.Empty;
    public DateTime ApplicationDate {get; set;} = DateTime.MaxValue;
    public DateTime ExpirationDate {get; set;} = DateTime.MaxValue;

}
