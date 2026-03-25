using Project2.ApiTests.Clients;
using Project2.ApiTests.Config;
using Project2.ApiTests.Constants;
using Project2.ApiTests.Helpers;
using Project2.ApiTests.Models.Requests;
using Project2.ApiTests.Models.Responses;

namespace Project2.ApiTests.Tests;

public class AuthTests
{
    private readonly ApiClient _apiClient;
    private readonly TestSettings _settings;

    public AuthTests()
    {
        _apiClient = new ApiClient();
        _settings = ConfigurationHelper.GetTestSettings();
    }

    [Fact]
    [Trait("Category", "Auth")]
    [Trait("Category", "Positive")]
    public async Task Login_WithValidCredentials_ShouldReturnAccessToken()
    {
        var loginRequest = new LoginRequest
        {
            Username = _settings.Username,
            Password = _settings.Password
        };

        var response = await _apiClient.PostAsync(ApiEndpoints.AuthLogin, loginRequest);

        Assert.Equal(200, (int)response.StatusCode);
        Assert.NotNull(response.Content);

        var loginResponse = JsonHelper.Deserialize<LoginResponse>(response.Content!);

        Assert.NotNull(loginResponse);
        Assert.False(string.IsNullOrWhiteSpace(loginResponse.AccessToken));
        Assert.Equal(_settings.Username, loginResponse.Username);
    }

    [Fact]
    [Trait("Category", "Auth")]
    [Trait("Category", "Positive")]
    public async Task GetCurrentUser_WithValidAccessToken_ShouldReturnAuthenticatedUser()
    {
        var loginRequest = new LoginRequest
        {
            Username = _settings.Username,
            Password = _settings.Password
        };

        var loginResponseRaw = await _apiClient.PostAsync(ApiEndpoints.AuthLogin, loginRequest);

        Assert.Equal(200, (int)loginResponseRaw.StatusCode);
        Assert.NotNull(loginResponseRaw.Content);

        var loginResponse = JsonHelper.Deserialize<LoginResponse>(loginResponseRaw.Content!);

        Assert.NotNull(loginResponse);
        Assert.False(string.IsNullOrWhiteSpace(loginResponse.AccessToken));

        var authenticatedClient = new AuthenticatedApiClient(loginResponse.AccessToken);

        var userResponseRaw = await authenticatedClient.GetAsync(ApiEndpoints.AuthMe);

        Assert.Equal(200, (int)userResponseRaw.StatusCode);
        Assert.NotNull(userResponseRaw.Content);

        var user = JsonHelper.Deserialize<UserResponse>(userResponseRaw.Content!);

        Assert.NotNull(user);
        Assert.Equal(_settings.Username, user.Username);
        Assert.False(string.IsNullOrWhiteSpace(user.Email));
    }

    [Fact]
    [Trait("Category", "Auth")]
    [Trait("Category", "Negative")]
    public async Task Login_WithInvalidPassword_ShouldReturnBadRequest()
    {
        var loginRequest = new LoginRequest
        {
            Username = _settings.Username,
            Password = "wrongpassword123"
        };

        var response = await _apiClient.PostAsync(ApiEndpoints.AuthLogin, loginRequest);

        Assert.Equal(400, (int)response.StatusCode);
        Assert.NotNull(response.Content);
    }

    [Fact]
    [Trait("Category", "Auth")]
    [Trait("Category", "Negative")]
    public async Task Login_WithMissingPassword_ShouldFail()
    {
        var loginRequest = new
        {
            Username = _settings.Username
        };

        var response = await _apiClient.PostAsync(ApiEndpoints.AuthLogin, loginRequest);

        Assert.NotEqual(200, (int)response.StatusCode);
    }
}