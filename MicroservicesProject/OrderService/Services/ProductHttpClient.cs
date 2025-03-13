using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace OrderService.Services
{
    public class ProductHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ProductHttpClient> _logger;

        public ProductHttpClient(HttpClient httpClient, ILogger<ProductHttpClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<bool> ValidateProductAsync(int productId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Products/{productId}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error validating product {ProductId}", productId);
                return false;
            }
        }
    }
}