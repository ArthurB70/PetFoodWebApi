using api.Models;
using Microsoft.EntityFrameworkCore;
namespace api;

public class ReminderRepository : IReminderRepository
{
   public ApplicationDBContext _context;
    public ReminderRepository(ApplicationDBContext context){
        _context = context;
    }

    public async Task<Reminder> CreateAsync(Reminder reminder)
    {
        await _context.Reminder.AddAsync(reminder);
        await _context.SaveChangesAsync();
        return reminder;
    }

    public async Task<List<Reminder>?> DeleteAllByUserIdAsync(int userId)
    {
        var reminders = await GetAllByUserIdAsync(userId);
        reminders?.ForEach(c => _context.Reminder.Remove(c));
        await _context.SaveChangesAsync();
        return reminders;
    }

    public async Task<Reminder?> DeleteAsync(int id)
    {
        var reminder = await _context.Reminder.FirstOrDefaultAsync(p => p.Id == id);
        if(reminder == null){
            return null;
        }
        _context.Reminder.Remove(reminder);
        await _context.SaveChangesAsync();
        return reminder;
    }

    public async Task<List<Reminder>> GetAllAsync()
    {
        return await _context.Reminder.ToListAsync();
    }

    public async Task<List<Reminder>?> GetAllByUserIdAsync(int userId)
    {
        var reminders =  await _context.Reminder.Where(p => p.UserId == userId).ToListAsync();
        
        return reminders;
    }

    public async Task<Reminder?> GetByIdAsync(int id)
    {
        return await _context.Reminder.FindAsync(id);
    }

    public async Task<Reminder?> UpdateAsync(int id, UpdateReminderDTO updateReminderDTO)
    {
        var reminder = await _context.Reminder.FirstOrDefaultAsync(p => p.Id == id);
        if(reminder == null){
            return null;
        }

        reminder.Name = updateReminderDTO.Name;
        reminder.Description = updateReminderDTO.Description;
        reminder.StartDate = updateReminderDTO.StartDate;
        reminder.EndDate = updateReminderDTO.EndDate;
        reminder.Color = updateReminderDTO.Color;

        await _context.SaveChangesAsync();
        return reminder;
    }
}
