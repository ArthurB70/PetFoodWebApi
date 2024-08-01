namespace api;

public interface IHardwareRepository
{
    Task<List<Hardware>> GetAllAsync();
    Task<Hardware?> GetByIdAsync(int id);
    Task<List<Hardware>?> GetAllByUserIdAsync(int userId);
    Task<List<Hardware>?> DeleteAllByUserIdAsync(int userId);
    Task<Hardware> CreateAsync(Hardware hardware);
    Task<Hardware?> UpdateAsync(int id, UpdateHardwareDTO updateHardwareDTO);
    Task<Hardware?> DeleteAsync(int id);
}
