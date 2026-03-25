namespace Project2.ApiTests.Models.Responses;

public class ProductsListResponse
{
    public List<ProductResponse> Products { get; set; } = new();
    public int Total { get; set; }
    public int Skip { get; set; }
    public int Limit { get; set; }
}