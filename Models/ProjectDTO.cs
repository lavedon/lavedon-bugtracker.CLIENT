#nullable disable warnings
using System.ComponentModel.DataAnnotations;

namespace Client;
public class ProjectDTO {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? UserCreatedId { get; set; }
        public object UserCreated { get; set; }
}
