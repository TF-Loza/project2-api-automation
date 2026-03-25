using Project2.ApiTests.Config;
using RestSharp;

namespace Project2.ApiTests.Clients;

public class AuthenticatedApiClient
{
    private readonly RestClient _client;
    private readonly string _token;

    public AuthenticatedApiClient(string token)
    {
        var settings = ConfigurationHelper.GetTestSettings();
        _client = new RestClient(settings.BaseUrl);
        _token = token;
    }

    public async Task<RestResponse> GetAsync(string endpoint)
    {
        var request = new RestRequest(endpoint, Method.Get);
        request.AddHeader("Authorization", $"Bearer {_token}");

        return await _client.ExecuteAsync(request);
    }
}