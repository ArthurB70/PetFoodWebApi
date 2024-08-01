using Microsoft.EntityFrameworkCore;

namespace api;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDBContext _context;
    
    public UserRepository(ApplicationDBContext context){
        _context = context;
    }
    public async Task<User> CreateAsync(User user)
    {
        await _context.User.AddAsync(user);
        await _context.SaveChangesAsync();
        return  user;
    }
    public async Task<User?> DeleteAsync(int id)
    {
        var user = await _context.User.FirstOrDefaultAsync(u => u.Id == id);
        if(user == null){
            return null;
        }
        _context.User.Remove(user);
        await _context.SaveChangesAsync();
        return user;
    }
    public async Task<List<User>> GetAllAsync(){
        return await _context.User.ToListAsync();
    }
    public async Task<User?> GetByIdAsync (int id){
        return await _context.User.FindAsync(id);
    }
    public async Task<User?> GetByEmailAsync(string email){
        return await _context.User.FirstOrDefaultAsync(u => u.Email == email);
    }
    public async Task<User?> UpdateAsync(int id, UpdateUserDTO updateUserDTO)
    {
        var user = await _context.User.FirstOrDefaultAsync(u => u.Id == id);

        if(user == null){
            return null;
        }
        
        user.Name = updateUserDTO.Name;
        user.SurName = updateUserDTO.SurName;
        user.Email = updateUserDTO.Email;
        user.Password = updateUserDTO.Password;

        await _context.SaveChangesAsync();
        return user;
    }
    public async Task<User?> GetAuthenticationAsync(string email){
        var user = await _context.User.FirstOrDefaultAsync(u => u.Email == email);

        return user;
    }
    public async Task<bool> UserExists(int id)
    {
        return await _context.User.AnyAsync(u => u.Id == id);
    }
}
