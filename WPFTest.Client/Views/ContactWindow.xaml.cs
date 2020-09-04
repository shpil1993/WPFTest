using System;
using System.Linq;
using System.Windows;
using WPFTest.Client.Enums;
using WPFTest.Client.Model;
using WPFTest.Client.Service;

namespace WPFTest.Client.Views
{
    /// <summary>
    /// Interaction logic for ContactWindow.xaml
    /// </summary>
    public partial class ContactWindow : Window
    {
        public Contact Contact { get; set; }

        public ContactWindow(int contactId, int personId)
        {
            InitializeComponent();
            contactTypeCombobox.ItemsSource = Enum.GetValues(typeof(ContactType)).Cast<ContactType>();
            LoadContact(personId, contactId);
        }

        private void LoadContact(int personId, int contactId)
        {
            if (contactId > 0)
            {
                var client = new HttpService();
                Contact = client.GetContact(contactId, personId);
                return;
            }
            Contact = new Contact()
            {
                PersonId = personId,
                ContactType = ContactType.Phone
            };
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Contact.Error))
            {
                var client = new HttpService();
                DialogResult = client.SaveContact(Contact); 
            }
            MessageBox.Show(Contact.Error);
        }
    }
}
