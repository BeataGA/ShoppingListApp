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
                BaseAddress = new Uri("http://localhost:5141/api/") 
            };
        }

        public async Task<List<ShoppingList>> GetShoppingListsAsync()
        {
            var response = await _httpClient.GetAsync("ShoppingList");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<ShoppingList>>(json);
        }

        public async Task SubmitShoppingListAsync(ShoppingList shoppingList)
        {
            var content = new StringContent(JsonSerializer.Serialize(shoppingList), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("ShoppingList", content);
            response.EnsureSuccessStatusCode();
        }
    }
}
