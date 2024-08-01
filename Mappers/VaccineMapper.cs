namespace api;

public static class VaccineMapper
{
    public static VaccineDTO ToVaccineDTO (this Vaccine vaccine){
        return new VaccineDTO {
            Id = vaccine.Id,
            Name = vaccine.Name,
            ApplicationDate = vaccine.ApplicationDate,
            ExpirationDate = vaccine.ExpirationDate,
            PetId = vaccine.PetId
        };
    } 

    public static Vaccine ToVaccineFromCreateVaccineDTO (this CreateVaccineDTO createVaccineDTO, int userId){
        return new Vaccine {
            Name = createVaccineDTO.Name,
            ApplicationDate = createVaccineDTO.ApplicationDate,
            ExpirationDate = createVaccineDTO.ExpirationDate,
            PetId = userId
        };
    }
}
