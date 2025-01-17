using ShoppingListApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ShoppingListApp.Services
{
    public class ShoppingListService
    {
        private readonly HttpClient _httpClient;

        public ShoppingListService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://your-api-url.com/") // Zastąp swoim adresem API
            };
        }

        public async Task<List<ShoppingList>> GetShoppingListsAsync()
        {
            var response = await _httpClient.GetAsync("shopping-lists");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<ShoppingList>>(json);
        }

        public async Task AddShoppingListAsync(string name)
        {
            var content = new StringContent(JsonSerializer.Serialize(new { Name = name }), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("shopping-lists", content);
            response.EnsureSuccessStatusCode();
        }
    }
}
