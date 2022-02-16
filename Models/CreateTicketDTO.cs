    using System.ComponentModel.DataAnnotations;

    namespace Client.Models;
    public class CreateTicketDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string? Project { get; set; }
        public object UserCreated { get; set; }
        public string? UserAssigned { get; set; }
    }
