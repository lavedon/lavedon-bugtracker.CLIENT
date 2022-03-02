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
        Console.WriteLine("Http Service created");
        Console.WriteLine(httpService);
        Console.WriteLine("About to get the official id of the selected project.");
        string uriProjectName = project.name;
        uriProjectName = uriProjectName.Replace(" ", "%20");
        Console.WriteLine("Formatted project name URI is: {0}", uriProjectName);
        var result = await httpService.Get<ProjectWithUserDTO>($"{APIEndpoints.s_getprojectbyname}/{uriProjectName}");
        Console.WriteLine("Project we got by name");
        Console.WriteLine("result:");
        Console.WriteLine(result);
        if (result != null)
        {
            Console.WriteLine("Result tested as not null");
            Console.WriteLine($"Got the project ID number, it is ${result.id}");
            project.id = result.id;
        }
        if (result is null) {
            Console.WriteLine("result was null");
            Console.WriteLine("Could not get the project ID number");
        }
        Console.WriteLine("About to try http put");
        Console.WriteLine("Sending this project to put method");
        Console.WriteLine(project);
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