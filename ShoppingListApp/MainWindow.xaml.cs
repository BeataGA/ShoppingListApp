using System.Windows;
using ShoppingListApp;
using ShoppingListApp.ViewModels;  

namespace ShoppingListApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = new MainViewModel();
        }
    }
}

