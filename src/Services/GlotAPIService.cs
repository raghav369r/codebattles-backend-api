using System.Text;
using System.Text.Json;
using CodeBattles_Backend.DTOs;

namespace CodeBattles_Backend.Services;

public class GlotAPIService
{
  private static string BASE_URL = "https://glot.io/api/run/";
  private readonly HttpClient _httpClient;
  public GlotAPIService(HttpClient httpClient, IConfiguration configuration)
  {
    string apiKey = configuration["Tokens:GLOT_API_TOKEN"] ?? "";
    _httpClient = httpClient;
    _httpClient.DefaultRequestHeaders.Authorization =
      new System.Net.Http.Headers.AuthenticationHeaderValue("Token", apiKey);
  }

  public async Task<RunCodeResponse?> RunCode(string code, string language, string? input)
  {
    string language_extension = language == "python" ? "py" : language;
    string url_with_language = BASE_URL + language + "/latest";
    var json = JsonSerializer.Serialize(
      new
      {
        stdin = input ?? "",
        files = new[] { new{
              name = "main."+language_extension,
              content = code }}
      });
    var content = new StringContent(json, Encoding.UTF8, "application/json");
    var response = await _httpClient.PostAsync(url_with_language, content);
    response.EnsureSuccessStatusCode();

    var responseStream = await response.Content.ReadAsStreamAsync();
    var result = await JsonSerializer.DeserializeAsync<RunCodeResponse>(responseStream);
    return result;
  }
}