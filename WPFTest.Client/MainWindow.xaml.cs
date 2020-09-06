using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFTest.Client.Enums;
using WPFTest.Client.Model;
using WPFTest.Client.Utils;
using WPFTest.Client.Views;
using WPFTestApp.Client.ViewModel;

namespace WPFTest.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            comboBoxLocales.ItemsSource = Enum.GetValues(typeof(Locale)).Cast<Locale>();
            comboboxPageSize.ItemsSource = new List<int>() { 10, 15, 20 }; 
        }

        private void OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            var displayName = Utilities.GetPropertyDisplayName(e.PropertyDescriptor);

            switch (e.Column.Header)
            {
                case "Greeting":
                    e.Cancel = true;
                    return;
                case "Country":
                    e.Cancel = true;
                    return;
                case "Gender":
                    e.Cancel = true;
                    return;
                case "Error":
                    e.Cancel = true;
                    return;
            }

            if (!string.IsNullOrEmpty(displayName))
            {
                e.Column.Header = displayName;
            }
        }

        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var grid = (DataGridRow)sender;
            var personWindow = new PersonWindow((int)comboBoxLocales.SelectedItem, (Person)grid.DataContext);

            personWindow.ShowDialog();
        }
    }
}
