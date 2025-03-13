using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using OrderService.Models.Dtos;

namespace OrderService.Services
{
    public class UserHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<UserHttpClient> _logger;

        public UserHttpClient(HttpClient httpClient, ILogger<UserHttpClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<bool> ValidateUserAsync(int userId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Users/{userId}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error validating user {UserId}", userId);
                return false;
            }
        }

        public async Task<UserDto> GetUserAsync(int userId)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<UserDto>($"api/Users/{userId}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting user {UserId}", userId);
                return null;
            }
        }
    }
}