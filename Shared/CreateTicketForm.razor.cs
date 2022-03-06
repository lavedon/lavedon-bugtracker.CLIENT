#nullable disable warnings 

using Client;
using Client.Shared;
using Client.Models;
using Client.Services;

using System.Net.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.JSInterop;

namespace Client.Shared
{
    public partial class CreateTicketFormCode : ComponentBase
    {

    [Inject]
    public IEntityService EntityService { get; set; }

    public enum EditOrCreate
    {
        Create,
        Edit
    }
    [Parameter] 
    public EditOrCreate SelectEditOrCreate { get; set; } = EditOrCreate.Create;

    [Parameter]
    public string CurrentUserAssigned { get; set; } = string.Empty;
    [Parameter]
    public string CurrentProjectAssigned { get; set; } = string.Empty;

    [Parameter] 
    public string ButtonText { get; set; } = "Create Ticket";

    [Parameter]
    public string CurrentUserName { get; set; } = string.Empty;
    [Parameter]
    public string PassedTitle { get; set; } = string.Empty;
    [Parameter]
    public string PassedDescription { get; set; } = string.Empty;

    [Parameter]
    public string PassedUserCreated { get; set; } = string.Empty;

    [Parameter]
    public int PassedTicketId { get; set; } = 0;
    [Parameter]
    public int PassedProjectId { get; set; } = 0;
    [Parameter]
    public int PassedAssignedUserId { get; set; } = 0;

    [Parameter]
    public int PassedUserCreatedId { get; set; } = 0;

    public CreateTicketDTO createTicketDTO = new CreateTicketDTO();
    public EditContext? EditContext;
    public string userAssigned = "";
    public string project = "";



    protected async override void OnInitialized()
    {
        createTicketDTO.Name = PassedTitle;
        createTicketDTO.Description = PassedDescription;
        // pass in the current user
        EditContext = new EditContext(createTicketDTO);

        await EntityService.GetProjects();
        await EntityService.GetUsers();
        StateHasChanged();
    }

    public async Task HandleSubmit()
    {
        if (SelectEditOrCreate == EditOrCreate.Create)
        {
            createTicketDTO.UserCreated = CurrentUserName;
            if (createTicketDTO.Project is null)
            {
                createTicketDTO.Project = EntityService.ProjectsWithUsers[0].name;
            }

            if (createTicketDTO.UserAssigned is null)
            {
                createTicketDTO.UserAssigned = EntityService.Users[0].username;            
            }

            await EntityService.CreateTicket(createTicketDTO);

            createTicketDTO.Name = string.Empty;
            createTicketDTO.Description = string.Empty;
            createTicketDTO.Project = string.Empty;
            createTicketDTO.UserAssigned = string.Empty;
        }
        if (SelectEditOrCreate == EditOrCreate.Edit)
        {
            createTicketDTO.UserCreated = "ignore";
            Console.WriteLine("EditForm CreateTickedDTO is");
            Console.WriteLine("Name: " + createTicketDTO.Name);
            Console.WriteLine("Description: " + createTicketDTO.Description);
            if (string.IsNullOrEmpty(createTicketDTO.UserAssigned))
            {
                createTicketDTO.UserAssigned = EntityService.Users[0].username;
            }
            Console.WriteLine("User Assigned: " + createTicketDTO.UserAssigned);
            if (string.IsNullOrEmpty(createTicketDTO.Project))
            {
                createTicketDTO.Project = EntityService.ProjectsWithUsers[0].name;
            }
            Console.WriteLine("Project: " + createTicketDTO.Project);
            Console.WriteLine("User Created: " + createTicketDTO.UserCreated);

            await EntityService.UpdateTicket(createTicketDTO, PassedTicketId);

            }

        }
    }
}