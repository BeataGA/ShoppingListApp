using ShoppingListApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace ShoppingListApp.Models
{
    public class ShoppingList
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("products")]
        public ObservableCollection<string> Products { get; set; } = new ObservableCollection<string>();
    }
}