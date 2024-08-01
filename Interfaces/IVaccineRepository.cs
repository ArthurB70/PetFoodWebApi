namespace api;

public interface IVaccineRepository
{
    Task<List<Vaccine>> GetAllAsync();
    Task<Vaccine?> GetByIdAsync(int id);
    Task<List<Vaccine>?> GetAllByPetIdAsync(int petId);
    Task<List<Vaccine>?> DeleteAllByPetIdAsync(int petId);
    Task<Vaccine> CreateAsync(Vaccine vaccine);
    Task<Vaccine?> UpdateAsync(int id, UpdateVaccineDTO updateVaccineDTO);
    Task<Vaccine?> DeleteAsync(int id);
}
