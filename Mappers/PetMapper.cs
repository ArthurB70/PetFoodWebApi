namespace api;

public static class PetMapper
{
    public static PetDTO ToPetDTO (this Pet pet){
        return new PetDTO{
            Id = pet.Id,
            Specie = pet.Specie,
            Name = pet.Name,
            Breed = pet.Breed,
            BirthDate = pet.BirthDate,
            Gender = pet.Gender,
            Size = pet.Size,
            UserId = pet.UserId
        };
    }
    public static Pet ToPetFromCreatePetDTO (this CreatePetDTO createPetDTO, int userId){
        return new Pet {
            Specie = createPetDTO.Specie,
            Name = createPetDTO.Name,
            Breed = createPetDTO.Breed,
            BirthDate = createPetDTO.BirthDate,
            Gender = createPetDTO.Gender,
            Size = createPetDTO.Size,
            UserId = userId
        };
    }
}
