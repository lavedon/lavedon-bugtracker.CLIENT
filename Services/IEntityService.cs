using Client.Models;

namespace Client.Services;

public interface IEntityService
{
    public List<ProjectWithUserDTO> ProjectsWithUsers { get; set; }
    public List<UserDTO> Users { get; set; }

    public List<TicketDTO> Tickets { get; set; }

    public Task CreateProject(ProjectDTO project);
    public Task CreateTicket(CreateTicketDTO ticket);
    public Task GetProjects();
    public Task GetUsers();

    public Task GetTickets();

}