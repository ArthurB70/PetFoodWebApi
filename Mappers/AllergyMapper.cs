namespace api;

public static class AllergyMapper
{
    public static AllergyDTO ToAllergyDTO(this Allergy allergy){
        return new AllergyDTO{
            Id = allergy.Id,
            Name = allergy.Name,
            Vaccinated = allergy.Vaccinated,
            PetId = allergy.PetId
        };
    }

    public static Allergy ToAllergyFromAllergyDTO(this CreateAllergyDTO createAllergyDTO, int petId){
        return new Allergy{
            Name = createAllergyDTO.Name,
            Vaccinated = createAllergyDTO.Vaccinated,
            PetId = petId
        };
    }
}
