using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WPFTest.Client.Model;
using WPFTest.Client.Service;

namespace WPFTestApp.Client.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private Person selectedPerson;

        public ObservableCollection<Person> People { get; set; }

        public Person SelectedPerson
        {
            get { return selectedPerson; }
            set
            {
                selectedPerson = value;
                OnPropertyChanged("SelectedPerson");
            }
        }

        public MainWindowViewModel()
        {
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
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
