using api.Models;

namespace api;

public interface IReminderRepository
{
    Task<List<Reminder>> GetAllAsync();
    Task<Reminder?> GetByIdAsync(int id);
    Task<List<Reminder>?> GetAllByUserIdAsync(int userId);
    Task<List<Reminder>?> DeleteAllByUserIdAsync(int userId);
    Task<Reminder> CreateAsync(Reminder reminder);
    Task<Reminder?> UpdateAsync(int id, UpdateReminderDTO updateReminderDTO);
    Task<Reminder?> DeleteAsync(int id);
}
