using System;
using System.Collections.Generic;
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
using WPFTest.Client.Utils;
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
        }

        private void OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            var displayName = Utilities.GetPropertyDisplayName(e.PropertyDescriptor);

            switch (e.Column.Header)
            {
                case "GreetingId":
                    e.Cancel = true;
                    return;
                case "CountryCode":
                    e.Cancel = true;
                    return;
                case "Gender":
                    e.Cancel = true;
                    return;
            }

            if (!string.IsNullOrEmpty(displayName))
            {
                e.Column.Header = displayName;
            }
        }
    }
}
