using Project2.ApiTests.Config;
using RestSharp;

namespace Project2.ApiTests.Clients;

public class ApiClient
{
    private readonly RestClient _client;

    public ApiClient()
    {
        var settings = ConfigurationHelper.GetTestSettings();
        _client = new RestClient(settings.BaseUrl);
    }

    public async Task<RestResponse> GetAsync(string endpoint)
    {
        var request = new RestRequest(endpoint, Method.Get);
        return await _client.ExecuteAsync(request);
    }

    public async Task<RestResponse> GetByIdAsync(string endpoint, int id)
    {
        var request = new RestRequest(endpoint, Method.Get);
        request.AddUrlSegment("id", id);

        return await _client.ExecuteAsync(request);
    }

    public async Task<RestResponse> PostAsync(string endpoint, object body)
    {
        var request = new RestRequest(endpoint, Method.Post);
        request.AddJsonBody(body);

        return await _client.ExecuteAsync(request);
    }
}