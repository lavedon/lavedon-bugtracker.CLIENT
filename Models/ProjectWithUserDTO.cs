namespace Client.Models;

public class UserCreated
{
    public int id { get; set; }
    public string username { get; set; }
    public string? password { get; set; }
    public string role { get; set; }
}

public class ProjectWithUserDTO
{
    public int id { get; set; }
    public string name { get; set; }
    public string description { get; set; }
    public int userCreatedId { get; set; }
    public UserCreated userCreated { get; set; }
}


