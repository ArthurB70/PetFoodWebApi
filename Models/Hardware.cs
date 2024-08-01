using System.Threading;

namespace api;

public class Hardware
{
    public int Id {get;set;}
    public string WaterSchedule {get; set;} = String.Empty;
    public string FoodSchedule {get; set;} = String.Empty;
    public int UserId {get; set;}
    //public User User = new User();
}