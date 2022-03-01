using Client.Models;

namespace Client.Services;

public interface IEntityService
{
    public List<ProjectWithUserDTO>? ProjectsWithUsers { get; set; } 
    public List<UserDTO> Users { get; set; }

    public List<TicketDTO> Tickets { get; set; }

    public Task CreateProject(ProjectDTO project);
    public Task UpdateProject(ProjectWithUserDTO project);
    public Task DeleteProject(int projectId);
    public Task CreateTicket(CreateTicketDTO ticket);
    public Task DeleteTicket(int ticketId);
    public Task GetProjects();
    public Task GetUsers();

    public Task GetTickets();

}