namespace api;

public interface IUserRepository
{
    Task<List<User>> GetAllAsync();
    Task<User?> GetByIdAsync(int id);
    Task<User?> GetByEmailAsync(string email);
    Task<User> CreateAsync(User user);
    Task<User?> UpdateAsync(int id, UpdateUserDTO updateUserDTO);
    Task<User?> DeleteAsync(int id);
    Task<bool> UserExists(int id); 
    Task<User?> GetAuthenticationAsync(string email);
}
