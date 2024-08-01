namespace api;

public class Pet
{
    public int Id {get; set;}
    public string Specie {get; set;} = String.Empty;
    public string Name {get; set;} = String.Empty;
    public string Breed {get; set;} = String.Empty;
    public DateTime BirthDate {get; set;} = DateTime.MaxValue;
    public string Gender {get; set;} = String.Empty;
    public string Size {get; set;} = String.Empty;
    public int UserId {get; set;}
    //public User User {get; set;} = new User();
    public List<Vaccine> Vaccines {get; set;} = new List<Vaccine>();
    public List<Allergy> Allergies {get; set;} = new List<Allergy>();
    public List<Medicine> Medicines {get; set;} = new List<Medicine>();
    public List<Comment> Comments {get; set;} = new List<Comment>();

}
