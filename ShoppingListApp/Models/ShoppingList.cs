using ShoppingListApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingListApp.Models
{
    public class ShoppingList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ShoppingItem> Items { get; set; }

        public ObservableCollection<string> Products { get; set; } = new ObservableCollection<string>();


    }
}










