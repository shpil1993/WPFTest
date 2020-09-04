using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WPFTest.Client.Model;
using WPFTest.Client.Service;

namespace WPFTest.Client.ViewModel.Person
{
    public class PersonWindowViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Country> Countries { get; set; }

        public ObservableCollection<Greeting> Greetings { get; set; }

        public PersonWindowViewModel(int lang)
        {

            LoadContent(lang);
        }

        private void LoadContent(int lang)
        {
            var client = new HttpService();
            Countries = new ObservableCollection<Country>(client.GetCountries(lang));
            Greetings = new ObservableCollection<Greeting>(client.GetGreetings(lang));
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
