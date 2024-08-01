using Microsoft.EntityFrameworkCore;

namespace api;

public class HardwareRepository : IHardwareRepository
{
   public ApplicationDBContext _context;
    public HardwareRepository(ApplicationDBContext context){
        _context = context;
    }

    public async Task<Hardware> CreateAsync(Hardware hardware)
    {
        await _context.Hardware.AddAsync(hardware);
        await _context.SaveChangesAsync();
        return hardware;
    }

    public async Task<List<Hardware>?> DeleteAllByUserIdAsync(int userId)
    {
        var hardwares = await GetAllByUserIdAsync(userId);
        hardwares?.ForEach(c => _context.Hardware.Remove(c));
        await _context.SaveChangesAsync();
        return hardwares;
    }

    public async Task<Hardware?> DeleteAsync(int id)
    {
        var hardware = await _context.Hardware.FirstOrDefaultAsync(p => p.Id == id);
        if(hardware == null){
            return null;
        }
        _context.Hardware.Remove(hardware);
        await _context.SaveChangesAsync();
        return hardware;
    }

    public async Task<List<Hardware>> GetAllAsync()
    {
        return await _context.Hardware.ToListAsync();
    }

    public async Task<List<Hardware>?> GetAllByUserIdAsync(int userId)
    {
        var hardwares =  await _context.Hardware.Where(p => p.UserId == userId).ToListAsync();
        
        return hardwares;
    }

    public async Task<Hardware?> GetByIdAsync(int id)
    {
        return await _context.Hardware.FindAsync(id);
    }



    public async Task<Hardware?> UpdateAsync(int id, UpdateHardwareDTO updateHardwareDTO)
    {
        var hardware = await _context.Hardware.FirstOrDefaultAsync(p => p.Id == id);
        if(hardware == null){
            return null;
        }

        hardware.WaterSchedule = updateHardwareDTO.WaterSchedule;
        hardware.FoodSchedule = updateHardwareDTO.FoodSchedule;

        await _context.SaveChangesAsync();
        return hardware;
    }
}
