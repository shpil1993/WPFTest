using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using WPFTest.Client.Command;
using WPFTest.Client.Enums;
using WPFTest.Client.Model;
using WPFTest.Client.Service;
using WPFTest.Client.Views;

namespace WPFTestApp.Client.ViewModel.Main
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private Person selectedPerson;
        private Locale locale;

        public ObservableCollection<Person> People { get; set; }

        public ICommand AddPerson { get; set; }

        public Person SelectedPerson
        {
            get { return selectedPerson; }
            set
            {
                if (selectedPerson == value)
                {
                    return;
                }
                selectedPerson = value;
                OnPropertyChanged("SelectedPerson");
            }
        }

        public Locale Locale
        {
            get { return locale; }
            set
            {
                if (locale == value)
                {
                    return;
                }
                locale = value;
                OnPropertyChanged("Locale");
            }
        }

        public MainWindowViewModel()
        {
            AddPerson = new CustomCommand(AddNewPerson);
            RefreshGrid();
        }

        private void RefreshGrid()
        {
            var service = new HttpService();
            People = new ObservableCollection<Person>(service.GetPeopleForTable(30, 0, 4, string.Empty));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private void AddNewPerson(object obj)
        {
            var personWindow = new PersonWindow((int)locale);

            personWindow.ShowDialog();
        }
    }
}
