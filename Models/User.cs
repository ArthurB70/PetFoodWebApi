namespace api;
using System;
using System.Collections.Generic;

public class User
{
    public int Id {get; set;}
    public string Name {get; set;} = String.Empty;
    public string SurName {get; set;} = String.Empty;
    public string Email {get; set;} = String.Empty;
    public string Password {get; set;} = String.Empty;
    public List<Pet> Pets {get; set;} = new List<Pet>();
    public List<Hardware> Hardwares {get; set;} = new List<Hardware>();
}
