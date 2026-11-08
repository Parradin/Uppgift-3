using System.Collections.Generic;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Client.Models;
using System.Net.Http.Json;

namespace Client.Services;

public class RestDbService
{
    private readonly HttpClient _httpClient;

    private const string BaseUrl =
        "https://www-westcoasteducation-be50.restdb.io/rest/";

    public RestDbService()
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri(BaseUrl)
        };

        _httpClient.DefaultRequestHeaders.Add(
            "x-apikey",
            "f008b4b87208ba41c611264757f9550bf7d50"
        );

        _httpClient.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json")
        );
    }
    public async Task<List<Course>> GetCoursesAsync()
    {
        var response = await _httpClient.GetFromJsonAsync<List<Course>>("courses");

        return response ?? new List<Course>();
    }
}