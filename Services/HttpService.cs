using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Text.Json;
using System.Threading.Tasks;
using Client.Models;


namespace Client.Services;

public class HttpService : IHttpService
{
    private HttpClient _httpClient;
    private NavigationManager _navigationManager;
    private ILocalStorageService _localStorageService;
    private IConfiguration _configuration;
    
    public HttpService(
        HttpClient httpClient,
        NavigationManager navigationManager,
        ILocalStorageService localStorageService,
        IConfiguration configuration
    ) {
        _httpClient = httpClient;
        _navigationManager = navigationManager;
        _localStorageService = localStorageService;
        _configuration = configuration;
    }
    public async Task<T> Get<T>(string uri)
    {
        
        HttpClient httpClient = new HttpClient();
        return await sendRequest<T>(uri, httpClient);
    }

    public async Task<T> Post<T>(string uri, object value)
    {
        var requestMessage = new HttpRequestMessage(HttpMethod.Post, uri);
        requestMessage.Content = new StringContent(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json");
        string token = await _localStorageService.GetItemAsync<string>("token");
        requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.Trim('"'));
        HttpClient httpClient = new HttpClient();
        requestMessage.Method = HttpMethod.Post;

        using var response = await httpClient.SendAsync(requestMessage);
        return default;
    }


    private async Task<T> sendRequest<T>(string uri, HttpClient _http)
    {
        string token = await _localStorageService.GetItemAsync<string>("token");

        if (!string.IsNullOrEmpty(token))
        {

            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Trim('"'));
            var response = await _http.GetFromJsonAsync<T>(uri);
            return response;
        }
        else
        {
            return default;
        }
    }
}