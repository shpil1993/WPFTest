using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using WPFTestApp.Client.Model;

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
            PopulateGrid();
        }

        private void PopulateGrid()
        {
            
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
