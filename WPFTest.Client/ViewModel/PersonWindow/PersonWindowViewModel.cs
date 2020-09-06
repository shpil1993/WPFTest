using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using WPFTest.Client.Command;
using WPFTest.Client.Model;
using WPFTest.Client.Service;
using WPFTest.Client.Views;

namespace WPFTest.Client.ViewModel.PersonWindow
{
    public class PersonWindowViewModel : INotifyPropertyChanged
    {
        private Person person;

        public ObservableCollection<Country> Countries { get; set; }

        public ObservableCollection<Greeting> Greetings { get; set; }

        public ObservableCollection<Contact> Contacts { get; set; }

        public ICommand SavePersonCommand { get; set; }
        public ICommand AddContactCommand { get; set; }
        public ICommand EditContactCommand { get; set; }
        public ICommand DeleteContactCommand { get; set; }

        public Person Person
        {
            get { return person; }
            set
            {
                if (person == value)
                {
                    return;
                }
                person = value;
                OnPropertyChanged("Person");
            }
        }

        public PersonWindowViewModel(int lang, Person person1 = null)
        {
            SavePersonCommand = new CustomCommand(SavePerson);
            AddContactCommand = new CustomCommand(AddContact);
            EditContactCommand = new CustomCommand(EditContact, (o) => { return o != null; });
            DeleteContactCommand = new CustomCommand(DeleteContact, CheckSelected);
            Person = person1 == null ? new Person() : person1;
            LoadContent(lang);
        }

        private void LoadContent(int lang)
        {
            var client = new HttpService();
            Countries = new ObservableCollection<Country>(client.GetCountries(lang));
            Greetings = new ObservableCollection<Greeting>(client.GetGreetings(lang));
            RefreshContacts();
        }

        private void RefreshContacts()
        {
            var client = new HttpService();
            if (Person.AddressNo > 0)
            {
                if (Contacts != null)
                {
                    Contacts.Clear();
                    foreach (var item in client.GetContactsForPerson(Person.AddressNo))
                    {
                        Contacts.Add(item);
                    }
                }
                else
                {
                    Contacts = new ObservableCollection<Contact>(client.GetContactsForPerson(Person.AddressNo));
                }
            }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private void SavePerson(object obj)
        {
            var client = new HttpService();
            var window = (Window)obj;
            if (!string.IsNullOrEmpty(Person.Error))
            {
                MessageBox.Show(Person.Error, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (client.SavePerson(Person))
            {
                window.DialogResult = true;
            }
        }

        private void AddContact(object obj)
        {
            var contactWindow = new ContactWindow(0, Person.AddressNo);

            contactWindow.ShowDialog();

            RefreshContacts();
        }

        private void EditContact(object obj)
        {
            var contact = (Contact)obj;
            var contactWindow = new ContactWindow(contact.PersonContactId, contact.PersonId);

            contactWindow.ShowDialog();

            RefreshContacts();
        }

        private void DeleteContact(object obj)
        {
            System.Collections.IList items = (System.Collections.IList)obj;
            var selected = items.Cast<Contact>();
            if (MessageBox.Show("Do you want to delete these contacts?", "Delete User Profiles", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                var service = new HttpService();
                service.DeleteContacts(Person.AddressNo, selected.Select(e => e.PersonContactId).ToList());
                RefreshContacts();
            }
        }

        private bool CheckSelected(object obj)
        {
            System.Collections.IList items = (System.Collections.IList)obj;
            if (items != null)
            {
                var selected = items.Cast<Contact>();
                return selected.Any();
            }
            return false;
        }
    }
}
