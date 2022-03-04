#nullable disable warnings
using Client.Models;
using Client.Static;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;


namespace Client.Services;

public class EntityService : IEntityService
{
    private readonly HttpClient _http;
    private readonly NavigationManager _navigationManager;
    private readonly ILocalStorageService _localStorageService;
    private readonly IConfiguration _configuration;

    public EntityService(HttpClient http, NavigationManager navigationManager, ILocalStorageService localStorageService, IConfiguration configuration)
    {
        _http = http;
        _navigationManager = navigationManager;
        _localStorageService = localStorageService;
        _configuration = configuration;
    }

    public List<ProjectWithUserDTO> ProjectsWithUsers { get; set; }
    public List<UserDTO> Users { get; set; }
    public List<TicketDTO> Tickets { get; set; }
    public ProjectDTO NewProject { get; set; }

    public async Task CreateProject(ProjectDTO project)
    {
        HttpService httpService = new HttpService(_http, _navigationManager, _localStorageService, _configuration);
        // await httpService.Post<ProjectDTO>("https://localhost:7266/api/createproject", project);
        await httpService.Post<ProjectDTO>(APIEndpoints.s_createproject, project);
    }

    public async Task DeleteProject(int projectId)
    {
        HttpService httpService = new HttpService(_http, _navigationManager, _localStorageService, _configuration);
        // await httpService.Delete<ProjectDTO>($"https://localhost:7266/api/deleteproject/{projectId}");
        await httpService.Delete<int>($"{APIEndpoints.s_deleteproject}/{projectId}");
    }

    public async Task UpdateProject(ProjectWithUserDTO project)
    {
        HttpService httpService = new HttpService(_http, _navigationManager, _localStorageService, _configuration);
        await httpService.Put<ProjectWithUserDTO>($"{APIEndpoints.s_updateproject}/{project.id}", project);
    }

    public async Task GetProjects()
    {

        Console.WriteLine("Inside GetProjects()");
        HttpService httpService = new HttpService(_http, _navigationManager, _localStorageService, _configuration);
        // var result = await httpService.Get<List<ProjectWithUserDTO>>("https://localhost:7266/api/getallprojects");
        var result = await httpService.Get<List<ProjectWithUserDTO>>(APIEndpoints.s_getallprojects);

        if (result != null)
        {
            ProjectsWithUsers = result;
        }
    }

    public async Task GetUsers()
    {
        HttpService httpService = new HttpService(_http, _navigationManager, _localStorageService, _configuration);
        // var result = await httpService.Get<List<UserDTO>>("https://localhost:7266/api/getallusers");
        var result = await httpService.Get<List<UserDTO>>(APIEndpoints.s_getallusers);
        if (result != null)
        {
            Users = result;
        }
    }

    public async Task CreateTicket(CreateTicketDTO ticket)
    {
        HttpService httpService = new HttpService(_http, _navigationManager, _localStorageService, _configuration);
        // await httpService.Post<CreateTicketDTO>("https://localhost:7266/api/createticket", ticket);
        await httpService.Post<CreateTicketDTO>(APIEndpoints.s_createticket, ticket);
    }

    public async Task UpdateTicket(CreateTicketDTO ticket, int ticketId)
    {
        TicketDTO ticketDTO = new TicketDTO();
        ticketDTO.Id = (int) ticketId;
        ticketDTO.Name = ticket.Name;
        ticketDTO.Description = ticket.Description;
        ticketDTO.ProjectId = 0;
        ticketDTO.UserCreatedId = 0;
        ticketDTO.UserAssignedId = 0;
        

        // ticketDTO.UserAssigned  = ticket.UserAssigned;
        HttpService httpService = new HttpService(_http, _navigationManager, _localStorageService, _configuration);
        await httpService.Put<TicketDTO>($"{APIEndpoints.s_updateticket}/{ticketId}", ticketDTO);
    }

    public async Task DeleteTicket(int ticketId)
    {
        HttpService httpService = new HttpService(_http, _navigationManager, _localStorageService, _configuration);
        // await httpService.Delete<int>("https://localhost:7266/api/deleteticket/" + ticketId);
        await httpService.Delete<int>(APIEndpoints.s_deleteticket + "/" + ticketId);
    }

    public async Task GetTickets()
    {
        HttpService httpService = new HttpService(_http, _navigationManager, _localStorageService, _configuration);
        // var result = await httpService.Get<List<TicketDTO>>("https://localhost:7266/api/getalltickets");
        var result = await httpService.Get<List<TicketDTO>>(APIEndpoints.s_getalltickets);
        if (result != null)
        {
            Tickets = result;
        }
    }
    
}