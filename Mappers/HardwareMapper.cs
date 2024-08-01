namespace api;

public static class HardwareMapper
{
    public static HardwareDTO ToHardwareDTO (this Hardware hardware){
        return new HardwareDTO {
            Id = hardware.Id,
            WaterSchedule = hardware.WaterSchedule,
            FoodSchedule = hardware.FoodSchedule,
            UserId = hardware.UserId
        };
    }

    public static Hardware ToHardwareFromCreateHardwareDTO (this CreateHardwareDTO createHardwareDTO, int userId){
        return new Hardware{
            WaterSchedule = createHardwareDTO.WaterSchedule,
            FoodSchedule = createHardwareDTO.FoodSchedule,
            UserId = userId
        };
    }
}
