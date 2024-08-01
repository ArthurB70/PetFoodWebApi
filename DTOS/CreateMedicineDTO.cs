namespace api;

public class CreateMedicineDTO
{
    public string Name {get; set;} = String.Empty;
    public decimal Dousage {get; set;}
    public string Type {get; set;} = String.Empty;
    public string MedicineSchedule { get; set; } = String.Empty;
}
