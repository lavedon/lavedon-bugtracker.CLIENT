    #nullable disable warnings
    using System.ComponentModel.DataAnnotations;

    namespace Client.Models;
    public partial class TicketDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } 
        [Required]
        public string Description { get; set; } 
        public int ProjectId { get; set; }
        public Project? Project { get; set; }
        public int? UserCreatedId { get; set; }
        public User? UserCreated { get; set; }
        public long UserAssignedId { get; set; }
        public User UserAssigned { get; set; } 
    }

    public partial class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } 
        public int UserCreatedId { get; set; } 
        public User UserCreated { get; set; } 
    }

    public partial class User
    {
        public int? Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string? Password { get; set; } = string.Empty;

        public string? Role { get; set; } = string.Empty;

    }