using Project2.ApiTests.Clients;
using Project2.ApiTests.Constants;

namespace Project2.ApiTests.Tests;

public class CartTests
{
    private readonly ApiClient _apiClient;

    public CartTests()
    {
        _apiClient = new ApiClient();
    }

    [Fact]
    [Trait("Category", "Carts")]
    [Trait("Category", "Positive")]
    public async Task GetAllCarts_ShouldReturnCarts()
    {
        var response = await _apiClient.GetAsync(ApiEndpoints.Carts);

        Assert.Equal(200, (int)response.StatusCode);
        Assert.NotNull(response.Content);

        using var json = System.Text.Json.JsonDocument.Parse(response.Content!);
        var root = json.RootElement;

        Assert.True(root.TryGetProperty("carts", out var carts));
        Assert.True(carts.GetArrayLength() > 0);
    }

    [Fact]
    [Trait("Category", "Carts")]
    [Trait("Category", "Negative")]
    public async Task GetCartByInvalidId_ShouldReturnNotFound()
    {
        int invalidId = 999999;

        var response = await _apiClient.GetByIdAsync(ApiEndpoints.CartById, invalidId);

        Assert.Equal(404, (int)response.StatusCode);
    }
}