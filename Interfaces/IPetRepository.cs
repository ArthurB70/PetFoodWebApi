namespace api;

public interface IPetRepository
{

    Task<List<Pet>> GetAllAsync();
    Task<Pet?> GetByIdAsync(int id);
    Task<List<Pet>?> GetAllByUserIdAsync(int userId);
    Task<Pet> CreateAsync(Pet pet);
    Task<Pet?> UpdateAsync(int id, UpdatePetDTO updatePetDTO);
    Task<Pet?> DeleteAsync(int id);
    Task<bool> PetExists(int id);
}
