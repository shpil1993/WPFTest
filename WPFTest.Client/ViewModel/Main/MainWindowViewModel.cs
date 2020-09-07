using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Data;
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
        private int pageSize = 15;
        private int page;
        private string search;
        private int count;

        public ObservableCollection<Person> People { get; set; }

        public ICommand AddPersonCommand { get; set; }
        public ICommand EditPersonCommand { get; set; }
        public ICommand DeletePersonCommand { get; set; }
        public ICommand RefreshPersonCommand { get; set; }
        public ICommand NextCommand { get; set; }
        public ICommand PrevCommand { get; set; }

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
                RefreshGrid(PageSize, Page, (int)locale, Search);
                OnPropertyChanged("Locale");
            }
        }

        public int PageSize
        {
            get { return pageSize; }
            set
            {
                if (pageSize == value)
                {
                    return;
                }
                pageSize = value;
                RefreshGrid(pageSize, Page, (int)Locale, Search);
                OnPropertyChanged("PageSize");
            }
        }

        public int Page
        {
            get { return page; }
            set
            {
                if (page == value)
                {
                    return;
                }
                page = value;

                OnPropertyChanged("Page");
            }
        }

        public string Search
        {
            get { return search; }
            set
            {
                if (search == value)
                {
                    return;
                }
                search = value;
                Page = 0;
                RefreshGrid(pageSize, page, (int)locale, search);
                OnPropertyChanged("Search");
            }
        }

        public int Count
        {
            get { return count; }
            set
            {
                if (count == value)
                {
                    return;
                }
                count = value;
                OnPropertyChanged("Count");
            }
        }

        public MainWindowViewModel()
        {
            AddPersonCommand = new CustomCommand(AddPerson);
            EditPersonCommand = new CustomCommand(EditPerson, (o) => { return o != null; });
            DeletePersonCommand = new CustomCommand(DeletePerson, CheckSelected);
            RefreshPersonCommand = new CustomCommand(RefreshPerson);
            PrevCommand = new CustomCommand(Prev, (o) => { return Page > 0; });
            NextCommand = new CustomCommand(Next, (o) => { return Page < Count; });
            RefreshGrid(PageSize, Page, (int)Locale, Search);
        }

        private async void RefreshGrid(int take, int skip, int lang, string search)
        {
            var service = new HttpService();
            if (People != null)
            {
                People.Clear();
                var res = await service.GetPeopleForTable(take, skip, lang, search);
                Count = (int)Math.Ceiling((double)(res.Count / PageSize));
                foreach (var item in res.People)
                {
                    People.Add(item);
                }
            }
            else
            {
                People = new ObservableCollection<Person>();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private void AddPerson(object obj)
        {
            var personWindow = new PersonWindow((int)locale);

            personWindow.ShowDialog();

            RefreshGrid(PageSize, Page, (int)Locale, Search);
        }

        private void EditPerson(object obj)
        {
            var personWindow = new PersonWindow((int)locale, (Person)obj);

            personWindow.ShowDialog();

            RefreshGrid(PageSize, Page, (int)Locale, Search);
        }

        private void DeletePerson(object obj)
        {
            System.Collections.IList items = (System.Collections.IList)obj;
            var selected = items.Cast<Person>();
            if (MessageBox.Show("Do you want to delete these user profiles?", "Delete User Profiles", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                var service = new HttpService();
                service.DeletePeople(selected.Select(e => e.AddressNo).ToList());
                RefreshGrid(PageSize, Page, (int)Locale, Search);
            }
        }

        private void RefreshPerson(object obj)
        {
            RefreshGrid(PageSize, Page, (int)Locale, Search);
        }

        private bool CheckSelected(object obj)
        {
            System.Collections.IList items = (System.Collections.IList)obj;
            if (items != null)
            {
                var selected = items.Cast<Person>();
                return selected.Any(); 
            }
            return false;
        }

        private void Prev(object obj)
        {
            Page = Page - 1;
            RefreshGrid(PageSize, Page, (int)Locale, Search);
        }

        private void Next(object obj)
        {
            Page = Page + 1;
            RefreshGrid(PageSize, Page, (int)Locale, Search);
        }
    }
}
