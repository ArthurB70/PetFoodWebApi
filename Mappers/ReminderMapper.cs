using api.Models;

namespace api;

public static class ReminderMapper
{
    public static ReminderDTO ToReminderDTO (this Reminder reminder){
        return new ReminderDTO {
            Id = reminder.Id,
            Name = reminder.Name,
            Description = reminder.Description,
            StartDate = reminder.StartDate,
            EndDate = reminder.EndDate,
            Color = reminder.Color,
            UserId = reminder.UserId
        };
    }

    public static Reminder ToReminderFromCreateReminderDTOR(this CreateReminderDTO createReminderDTO, int userId){
        return new Reminder {
            Name = createReminderDTO.Name,
            Description = createReminderDTO.Description,
            StartDate = createReminderDTO.StartDate,
            EndDate = createReminderDTO.EndDate,
            Color = createReminderDTO.Color,
            UserId = userId
        };
    }
}
