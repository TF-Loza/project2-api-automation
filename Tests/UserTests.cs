using Project2.ApiTests.Clients;
using Project2.ApiTests.Constants;
using Project2.ApiTests.Helpers;
using Project2.ApiTests.Models.Responses;

namespace Project2.ApiTests.Tests;

public class UserTests
{
    private readonly ApiClient _apiClient;

    public UserTests()
    {
        _apiClient = new ApiClient();
    }

    [Fact]
    [Trait("Category", "Users")]
    [Trait("Category", "Positive")]
    public async Task GetAllUsers_ShouldReturnUsers()
    {
        var response = await _apiClient.GetAsync(ApiEndpoints.Users);

        Assert.Equal(200, (int)response.StatusCode);
        Assert.NotNull(response.Content);

        using var json = System.Text.Json.JsonDocument.Parse(response.Content!);
        var root = json.RootElement;

        Assert.True(root.TryGetProperty("users", out var users));
        Assert.True(users.GetArrayLength() > 0);
    }

    [Fact]
    [Trait("Category", "Users")]
    [Trait("Category", "Positive")]
    public async Task GetUserById_ShouldReturnCorrectUser()
    {
        int userId = 1;

        var response = await _apiClient.GetByIdAsync(ApiEndpoints.UserById, userId);

        Assert.Equal(200, (int)response.StatusCode);
        Assert.NotNull(response.Content);

        var user = JsonHelper.Deserialize<UserResponse>(response.Content!);

        Assert.NotNull(user);
        Assert.Equal(userId, user.Id);
        Assert.False(string.IsNullOrWhiteSpace(user.Username));
    }

    [Fact]
    [Trait("Category", "Users")]
    [Trait("Category", "Negative")]
    public async Task GetUserByInvalidId_ShouldReturnNotFound()
    {
        int invalidId = 999999;

        var response = await _apiClient.GetByIdAsync(ApiEndpoints.UserById, invalidId);

        Assert.Equal(404, (int)response.StatusCode);
    }
}