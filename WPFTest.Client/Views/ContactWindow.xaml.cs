using System;
using System.Linq;
using System.Windows;
using WPFTest.Client.Enums;
using WPFTest.Client.Model;
using WPFTest.Client.Service;
using WPFTest.Client.ViewModel;

namespace WPFTest.Client.Views
{
    /// <summary>
    /// Interaction logic for ContactWindow.xaml
    /// </summary>
    public partial class ContactWindow : Window
    {
        public ContactWindow(int contactId, int personId)
        {
            InitializeComponent();
            contactTypeCombobox.ItemsSource = Enum.GetValues(typeof(ContactType)).Cast<ContactType>();
            DataContext = new ContactWindowViewModel(personId, contactId);
        }
    }
}
