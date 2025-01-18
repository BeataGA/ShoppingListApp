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
                BaseAddress = new Uri("http://localhost:5141") // Zastąp swoim adresem API
            };
        }

        public async Task<List<ShoppingList>> GetShoppingListsAsync()
        {
            var response = await _httpClient.GetAsync("api/ShoppingList");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            // Deserialize to ShoppingList, ensuring Items is populated with ShoppingItem objects
            var shoppingLists = JsonSerializer.Deserialize<List<ShoppingList>>(json);

            // Ensure Products is initialized for each shopping list
            foreach (var list in shoppingLists)
            {
                if (list.Items == null)
                {
                    list.Items = new List<ShoppingItem>();
                }
            }

            return shoppingLists;
        }


        public async Task AddShoppingListAsync(string name)
        {
            var content = new StringContent(JsonSerializer.Serialize(new { Name = name }), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("shopping-lists", content);
            response.EnsureSuccessStatusCode();
        }
    }
}
