#nullable disable warnings
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Text.Json;
using System.Net.Http.Headers;


namespace Client;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
        private readonly ILocalStorageService _localStorage;
        private readonly HttpClient _http;
        public string UserName { get; set; } 
    
        public CustomAuthStateProvider(ILocalStorageService localStorage, HttpClient http)
        {
            _localStorage = localStorage;
            _http = http;
            UserName = string.Empty;
        }
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        string token = await _localStorage.GetItemAsStringAsync("token");


        var identity = new ClaimsIdentity();

        if (!string.IsNullOrEmpty(token))
        {
            identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");
            string cleanedToken = token.Replace("\"", "");
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", cleanedToken.Replace("'", ""));
        }

        var user = new ClaimsPrincipal(identity);
        var userId = user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
        if (userId is not null)
        {
            await _localStorage.SetItemAsync<string>("usernameFromClaims", userId.Value);
        }
        var state = new AuthenticationState(user);

        NotifyAuthenticationStateChanged(Task.FromResult(state));

        return state;
    }

    public static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var payload = jwt.Split('.')[1];
        var jsonBytes = ParseBase64WithoutPadding(payload);
        var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

        return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
    }

    public static byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2: base64 += "=="; break;
            case 3: base64 += "="; break;
        }
        return Convert.FromBase64String(base64);
    }


}