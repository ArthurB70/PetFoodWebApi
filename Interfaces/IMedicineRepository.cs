namespace api;

public interface IMedicineRepository
{
    Task<List<Medicine>> GetAllAsync();
    Task<Medicine?> GetByIdAsync(int id);
    Task<List<Medicine>?> GetAllByPetIdAsync(int petId);
    Task<List<Medicine>?> DeleteAllByPetIdAsync(int petId);
    Task<Medicine> CreateAsync(Medicine medicine);
    Task<Medicine?> UpdateAsync(int id, UpdateMedicineDTO updateMedicineDTO);
    Task<Medicine?> DeleteAsync(int id);
}
