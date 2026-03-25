using Project2.ApiTests.Clients;
using Project2.ApiTests.Constants;
using Project2.ApiTests.Helpers;
using Project2.ApiTests.Models.Responses;

namespace Project2.ApiTests.Tests;

public class ProductTests
{
    private readonly ApiClient _apiClient;

    public ProductTests()
    {
        _apiClient = new ApiClient();
    }

    [Fact]
    [Trait("Category", "Products")]
    [Trait("Category", "Positive")]
    public async Task GetAllProducts_ShouldReturn200AndNonEmptyList()
    {
        var response = await _apiClient.GetAsync(ApiEndpoints.Products);

        Assert.Equal(200, (int)response.StatusCode);
        Assert.NotNull(response.Content);

        var result = JsonHelper.Deserialize<ProductsListResponse>(response.Content!);

        Assert.NotNull(result);
        Assert.NotNull(result.Products);
        Assert.True(result.Products.Count > 0);
        Assert.True(result.Total > 0);
    }

    [Fact]
    [Trait("Category", "Products")]
    [Trait("Category", "Positive")]
    public async Task GetProductById_ShouldReturnCorrectProduct()
    {
        int productId = 1;

        var response = await _apiClient.GetByIdAsync(ApiEndpoints.ProductById, productId);

        Assert.Equal(200, (int)response.StatusCode);
        Assert.NotNull(response.Content);

        var product = JsonHelper.Deserialize<ProductResponse>(response.Content!);

        Assert.NotNull(product);
        Assert.Equal(productId, product.Id);
        Assert.False(string.IsNullOrWhiteSpace(product.Title));
        Assert.True(product.Price > 0);
    }

    [Fact]
    [Trait("Category", "Products")]
    [Trait("Category", "Negative")]
    public async Task GetProductByInvalidId_ShouldReturnNotFound()
    {
        int invalidId = 999999;

        var response = await _apiClient.GetByIdAsync(ApiEndpoints.ProductById, invalidId);

        Assert.Equal(404, (int)response.StatusCode);
    }

    [Fact]
    [Trait("Category", "Products")]
    [Trait("Category", "Negative")]
    public async Task Get_InvalidEndpoint_ShouldReturnNotFound()
    {
        var response = await _apiClient.GetAsync("/invalid-endpoint");

        Assert.Equal(404, (int)response.StatusCode);
    }
}