namespace api;

public static class MedicineMapper
{
    public static MedicineDTO ToMedicineDTO (this Medicine medicine){
        return new MedicineDTO {
            Id = medicine.Id,
            Name = medicine.Name,
            Dousage = medicine.Dousage,
            Type = medicine.Type,
            MedicineSchedule = medicine.MedicineSchedule,
            PetId = medicine.PetId
        };
    }

    public static Medicine ToMedicineFromCreateMedicineDTO (this CreateMedicineDTO createMedicineDTO, int petId){
        return new Medicine{
            Name = createMedicineDTO.Name,
            Dousage = createMedicineDTO.Dousage,
            Type = createMedicineDTO.Type,
            MedicineSchedule = createMedicineDTO.MedicineSchedule,
            PetId = petId
        };
    }
}
