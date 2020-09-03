﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace WPFTestApp.Client.Model
{
    public class Person : INotifyPropertyChanged
    {
        private int addressNo;
        private string company;
        private string greeting;
        private string title;
        private string firstName;
        private string lastName;
        private string street;
        private string country;
        private string postalCode;
        private string city;
        private string phone;
        private DateTimeOffset? birthday;
        private DateTimeOffset? registration;

        [Display(Name = "Address No")]
        public int AddressNo
        {
            get { return addressNo; }
            set
            {
                addressNo = value;
                OnPropertyChanged("AddressNo");
            }
        }

        public string Company
        {
            get { return company; }
            set
            {
                company = value;
                OnPropertyChanged("Company");
            }
        }

        public string Greeting
        {
            get { return greeting; }
            set
            {
                greeting = value;
                OnPropertyChanged("Greeting");
            }
        }

        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                OnPropertyChanged("Title");
            }
        }

        [Display(Name = "First Name")]
        public string FirstName
        {
            get { return firstName; }
            set
            {
                firstName = value;
                OnPropertyChanged("FirstName");
            }
        }

        [Display(Name = "Last Name")]
        public string LastName
        {
            get { return lastName; }
            set
            {
                lastName = value;
                OnPropertyChanged("LastName");
            }
        }

        public string Street
        {
            get { return street; }
            set
            {
                street = value;
                OnPropertyChanged("Street");
            }
        }

        public string Country
        {
            get { return country; }
            set
            {
                country = value;
                OnPropertyChanged("Country");
            }
        }

        [Display(Name = "Postal Code")]
        public string PostalCode
        {
            get { return postalCode; }
            set
            {
                postalCode = value;
                OnPropertyChanged("PostalCode");
            }
        }

        public string City
        {
            get { return city; }
            set
            {
                city = value;
                OnPropertyChanged("City");
            }
        }

        public string Phone
        {
            get { return phone; }
            set
            {
                phone = value;
                OnPropertyChanged("Phone");
            }
        }

        public DateTimeOffset? Birthday
        {
            get { return birthday; }
            set
            {
                birthday = value;
                OnPropertyChanged("Birthday");
            }
        }

        public DateTimeOffset? Registration
        {
            get { return registration; }
            set
            {
                registration = value;
                OnPropertyChanged("Registration");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
