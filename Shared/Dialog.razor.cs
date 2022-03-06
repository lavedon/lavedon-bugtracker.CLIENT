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
    public partial class DialogCode : ComponentBase 
    {
        [Parameter]
        public bool Show { get; set; } = false;

        [Parameter]
        public RenderFragment Title { get; set; }

        [Parameter]
        public RenderFragment Body { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> OnOk { get; set; }
    }

}