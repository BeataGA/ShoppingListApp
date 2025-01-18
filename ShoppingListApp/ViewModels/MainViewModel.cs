using ShoppingListApp.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ShoppingListApp.Services;

namespace ShoppingListApp.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public ObservableCollection<ShoppingList> ShoppingLists { get; set; } = new ObservableCollection<ShoppingList>();

        private string _newProductName;
        public string NewProductName
        {
            get { return _newProductName; }
            set
            {
                if (_newProductName != value)
                {
                    _newProductName = value;
                    OnPropertyChanged(nameof(NewProductName));
                }
            }
        }

        private string _newShoppingListName;
        public string NewShoppingListName
        {
            get { return _newShoppingListName; }
            set
            {
                if (_newShoppingListName != value)
                {
                    _newShoppingListName = value;
                    OnPropertyChanged(nameof(NewShoppingListName));
                }
            }
        }

        private ShoppingList _selectedShoppingList;
        public ShoppingList SelectedShoppingList
        {
            get { return _selectedShoppingList; }
            set
            {
                if (_selectedShoppingList != value)
                {
                    _selectedShoppingList = value;
                    OnPropertyChanged(nameof(SelectedShoppingList));
                    OnPropertyChanged(nameof(SelectedShoppingList.Items)); // Ensure UI updates when the shopping list changes
                }
            }
        }

        private string _selectedProduct;
        public string SelectedProduct
        {
            get { return _selectedProduct; }
            set
            {
                if (_selectedProduct != value)
                {
                    _selectedProduct = value;
                    OnPropertyChanged(nameof(SelectedProduct));
                }
            }
        }

        public ICommand AddProductCommand { get; }
        public ICommand RemoveProductCommand { get; }
        public ICommand AddShoppingListCommand { get; }
        public ICommand RefreshCommand { get; }  

        private readonly ShoppingListService _shoppingListService;

        public MainViewModel()
        {
            _shoppingListService = new ShoppingListService();
            AddProductCommand = new RelayCommand(AddProduct);
            RemoveProductCommand = new RelayCommand(RemoveProduct);
            AddShoppingListCommand = new RelayCommand(AddShoppingList);
            RefreshCommand = new RelayCommand(RefreshShoppingLists);
            LoadShoppingLists();
        }

        private async Task LoadShoppingLists()
        {
            var shoppingLists = await _shoppingListService.GetShoppingListsAsync();
            foreach (var list in shoppingLists)
            {
                // Ensure Items is initialized for each shopping list
                if (list.Items == null)
                {
                    list.Items = new List<ShoppingItem>();
                }

                // Sample data for testing
                if (list.Name == "MyTestList")
                {
                    list.Items.Add(new ShoppingItem { Name = "Milk" });
                    list.Items.Add(new ShoppingItem { Name = "Eggs" });
                }

                ShoppingLists.Add(list);
            }
        }


        private async void RefreshShoppingLists()
        {
            ShoppingLists.Clear();  
            await LoadShoppingLists();  
        }

        private void AddProduct()
        {
            if (SelectedShoppingList != null && !string.IsNullOrEmpty(NewProductName))
            {
                // Ensure Items is initialized
                if (SelectedShoppingList.Items == null)
                {
                    SelectedShoppingList.Items = new List<ShoppingItem>();
                }

                SelectedShoppingList.Items.Add(new ShoppingItem { Name = NewProductName });
                NewProductName = string.Empty;
            }
        }

        private void RemoveProduct()
        {
            if (SelectedShoppingList != null && SelectedProduct != null)
            {
                var itemToRemove = SelectedShoppingList.Items.FirstOrDefault(i => i.Name == SelectedProduct);
                if (itemToRemove != null)
                {
                    SelectedShoppingList.Items.Remove(itemToRemove);
                    SelectedProduct = null;
                }
            }
        }

        private void AddShoppingList()
        {
            if (!string.IsNullOrEmpty(NewShoppingListName))
            {
                ShoppingLists.Add(new ShoppingList { Name = NewShoppingListName, Items = new List<ShoppingItem>() });
                NewShoppingListName = string.Empty;
            }
        }
    }
}
