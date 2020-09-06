using System;
using System.Linq;
using System.Windows;
using WPFTest.Client.Enums;
using WPFTest.Client.Model;
using WPFTest.Client.ViewModel.PersonWindow;

namespace WPFTest.Client.Views
{
    /// <summary>
    /// Interaction logic for PersonWindow.xaml
    /// </summary>
    public partial class PersonWindow : Window
    {
        public PersonWindow(int lang, Person person = null)
        {
            InitializeComponent();
            genderComboBox.ItemsSource = Enum.GetValues(typeof(Gender)).Cast<Gender>();
            contactsPanel.Visibility = person == null ? Visibility.Hidden : Visibility.Visible;
            DataContext = new PersonWindowViewModel(lang, person);
        }
    }
}
