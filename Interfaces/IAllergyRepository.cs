namespace api;

public interface IAllergyRepository
{
    Task<List<Allergy>> GetAllAsync();
    Task<Allergy?> GetByIdAsync(int id);
    Task<List<Allergy>?> GetAllByPetIdAsync(int petId);
    Task<List<Allergy>?> DeleteAllByPetIdAsync(int petId);
    Task<Allergy> CreateAsync(Allergy allergy);
    Task<Allergy?> UpdateAsync(int id, UpdateAllergyDTO updateAllergyDTO);
    Task<Allergy?> DeleteAsync(int id);
}
