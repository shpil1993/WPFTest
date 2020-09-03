﻿using Microsoft.OData.Client;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WPFTestApp.Client.Model;
using WPFTestApp.Client.Services;

namespace WPFTestApp.Client.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private Person selectedPerson;

        private DataServiceQueryContinuation<Models.Person> nextLink;

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
            var client = new ODataService();
            var people = client.GetPeopleForTableAsync().Result;
            nextLink = people.token;
            People = new ObservableCollection<Person>(people.people);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
