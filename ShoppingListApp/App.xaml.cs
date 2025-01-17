using System.Configuration;
using System.Data;
using System.Windows;
using System;


namespace ShoppingListApp
{
    public partial class App : Application
    {
        public App()
        {
            // Dodaj globalną obsługę błędów
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is Exception ex)
            {
                // Możesz wyświetlić szczegóły błędu w oknie dialogowym
                MessageBox.Show($"Niewykryty błąd: {ex.Message}\n{ex.StackTrace}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
