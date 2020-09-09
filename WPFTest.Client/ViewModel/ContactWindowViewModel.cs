using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Input;
using WPFTest.Client.Command;
using WPFTest.Client.Enums;
using WPFTest.Client.Model;
using WPFTest.Client.Service;

namespace WPFTest.Client.ViewModel
{
    public class ContactWindowViewModel : INotifyPropertyChanged
    {
        private Contact contact;

        public ICommand SaveContactCommand { get; set; }

        public Contact Contact
        {
            get { return contact; }
            set
            {
                if (contact == value)
                {
                    return;
                }
                contact = value;
                OnPropertyChanged("Contact");
            }
        }

        public ContactWindowViewModel(int personId, int contactId)
        {
            SaveContactCommand = new CustomCommand(SaveContact);
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
                ContactType = ContactType.Phone,
                ContactValue = string.Empty
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private void SaveContact(object obj)
        {
            var client = new HttpService();
            var window = (Window)obj;
            if (!string.IsNullOrEmpty(Contact.Error))
            {
                MessageBox.Show(Contact.Error, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            window.DialogResult = client.SaveContact(Contact);
        }
    }
}
