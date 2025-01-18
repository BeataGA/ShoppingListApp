using ShoppingListApp.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ShoppingListApp.Services;

namespace ShoppingListApp.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public ObservableCollection<ShoppingList> ShoppingLists { get; set; } = new ObservableCollection<ShoppingList>();
        private readonly ShoppingListService _shoppingListService;

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
        public ICommand SubmitShoppingListCommand { get; } 

        public MainViewModel()
        {
            _shoppingListService = new ShoppingListService();

            AddProductCommand = new RelayCommand(AddProduct);
            RemoveProductCommand = new RelayCommand(RemoveProduct);
            AddShoppingListCommand = new RelayCommand(AddShoppingList);
            RefreshCommand = new RelayCommand(Refresh);
            SubmitShoppingListCommand = new RelayCommand(SubmitShoppingList);
            LoadShoppingLists();
        }

        public async void LoadShoppingLists()
        {
            var shoppingLists = await _shoppingListService.GetShoppingListsAsync();
            System.Diagnostics.Debug.WriteLine($"Loaded {shoppingLists.Count} shopping lists.");
            ShoppingLists.Clear();
            foreach (var list in shoppingLists)
            {
                System.Diagnostics.Debug.WriteLine($"List: {list.Name}, Products: {string.Join(", ", list.Products)}");
                ShoppingLists.Add(list);
            }
        }

        private void Refresh()
        {
            LoadShoppingLists();
        }

        private void AddProduct()
        {
            if (SelectedShoppingList != null && !string.IsNullOrEmpty(NewProductName))
            {
                if (SelectedShoppingList.Products == null)
                {
                    SelectedShoppingList.Products = new ObservableCollection<string>();
                }

                SelectedShoppingList.Products.Add(NewProductName);
                NewProductName = string.Empty;
            }
        }

        private void RemoveProduct()
        {
            if (SelectedShoppingList != null && SelectedProduct != null)
            {
                SelectedShoppingList.Products.Remove(SelectedProduct);
                SelectedProduct = null;
            }
        }

        private void AddShoppingList()
        {
            if (!string.IsNullOrEmpty(NewShoppingListName))
            {
                ShoppingLists.Add(new ShoppingList { Name = NewShoppingListName, Products = new ObservableCollection<string>() });
                NewShoppingListName = string.Empty;
            }
        }

        private async void SubmitShoppingList()
                {
                    if (SelectedShoppingList != null)
                    {
                        try
                        {
                            await _shoppingListService.SubmitShoppingListAsync(SelectedShoppingList);
                            LoadShoppingLists();
                        }
                        catch (Exception ex)
                        {
                            System.Diagnostics.Debug.WriteLine($"Error submitting shopping list: {ex.Message}");
                        }
                    }
                }        
        
    }
}
