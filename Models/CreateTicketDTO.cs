using System.ComponentModel.DataAnnotations;
using Client.Services;

namespace Client.Models;
public class CreateTicketDTO
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Project { get; set; }
    public object? UserCreated { get; set; }
    public string? UserAssigned { get; set; }    
}
