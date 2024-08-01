namespace api;

public static class UserMapper
{
    public static UserDTO ToUserDTO (this User user){
        return new UserDTO{
            Id = user.Id,
            Name = user.Name,
            SurName = user.SurName,
            Email = user.Email
        };
    }

    public static User ToUserFromCreateUserDTO (this CreateUserDTO createUserDTO){
        return new User{
            Name = createUserDTO.Name,
            SurName = createUserDTO.SurName,
            Email = createUserDTO.Email,
            Password = createUserDTO.Password
        };
    }
}
