using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using WPFTest.Client.Enums;

namespace WPFTest.Client.Model
{
    public class Person : INotifyPropertyChanged, IDataErrorInfo
    {
        private int addressNo;
        private string company;
        private Greeting greeting;
        private string greetingText;
        private string title;
        private string firstName;
        private string lastName;
        private string street;
        private Country country;
        private string countryName;
        private string postalCode;
        private string city;
        private string phone;
        private string birthday;
        private string registration;
        private Gender gender;

        public string this[string columnName]
        {
            get
            {
                Error = string.Empty;
                switch (columnName)
                {
                    case "FirstName":
                        if (string.IsNullOrEmpty(FirstName))
                        {
                            Error = "First name is required!"; 
                        }
                        break;
                    case "LastName":
                        if (string.IsNullOrEmpty(FirstName))
                        {
                            Error = "Last name is required!"; 
                        }
                        break;
                    case "Greeting":
                        if (Greeting == null)
                        {
                            Error = "Greeting is required!"; 
                        }
                        break;
                    case "Country":
                        if (Country == null)
                        {
                            Error = "Country is required!"; 
                        }
                        break;
                    case "City":
                        if (City == null)
                        {
                            Error = "City is required!"; 
                        }
                        break;
                    default:
                        break;
                }
                return Error;
            }
        }

        public string Error { get; set; }

        [DisplayName("Address No")]
        public int AddressNo
        {
            get { return addressNo; }
            set
            {
                if (addressNo == value)
                {
                    return;
                }
                addressNo = value;
                OnPropertyChanged("AddressNo");
            }
        }

        public string Company
        {
            get { return company; }
            set
            {
                if (company == value)
                {
                    return;
                }
                company = value;
                OnPropertyChanged("Company");
            }
        }

        public Greeting Greeting
        {
            get { return greeting; }
            set
            {
                if (greeting == value)
                {
                    return;
                }
                greeting = value;
                OnPropertyChanged("Greeting");
            }
        }

        [DisplayName("Greeting")]
        public string GreetingText
        {
            get { return greetingText; }
            set
            {
                if (greetingText == value)
                {
                    return;
                }
                greetingText = value;
                OnPropertyChanged("GreetingText");
            }
        }

        public string Title
        {
            get { return title; }
            set
            {
                if (title == value)
                {
                    return;
                }
                title = value;
                OnPropertyChanged("Title");
            }
        }

        [DisplayName("First Name")]
        public string FirstName
        {
            get { return firstName; }
            set
            {
                if (firstName == value)
                {
                    return;
                }
                firstName = value;
                OnPropertyChanged("FirstName");
            }
        }

        [DisplayName("Last Name")]
        public string LastName
        {
            get { return lastName; }
            set
            {
                if (lastName == value)
                {
                    return;
                }
                lastName = value;
                OnPropertyChanged("LastName");
            }
        }

        public string Street
        {
            get { return street; }
            set
            {
                if (street == value)
                {
                    return;
                }
                street = value;
                OnPropertyChanged("Street");
            }
        }

        public Country Country
        {
            get { return country; }
            set
            {
                if (country == value)
                {
                    return;
                }
                country = value;
                OnPropertyChanged("Country");
            }
        }

        [DisplayName("Country")]
        public string CountryName
        {
            get { return countryName; }
            set
            {
                if (countryName == value)
                {
                    return;
                }
                countryName = value;
                OnPropertyChanged("CountryName");
            }
        }

        [DisplayName("Postal Code")]
        public string PostalCode
        {
            get { return postalCode; }
            set
            {
                if (postalCode == value)
                {
                    return;
                }
                postalCode = value;
                OnPropertyChanged("PostalCode");
            }
        }

        public string City
        {
            get { return city; }
            set
            {
                if (city == value)
                {
                    return;
                }
                city = value;
                OnPropertyChanged("City");
            }
        }

        public string Phone
        {
            get { return phone; }
            set
            {
                if (phone == value)
                {
                    return;
                }
                phone = value;
                OnPropertyChanged("Phone");
            }
        }

        public string Birthday
        {
            get { return birthday; }
            set
            {
                if (birthday == value)
                {
                    return;
                }
                birthday = value;
                OnPropertyChanged("Birthday");
            }
        }

        public string Registration
        {
            get { return registration; }
            set
            {
                if (registration == value)
                {
                    return;
                }
                registration = value;
                OnPropertyChanged("Registration");
            }
        }

        public Gender Gender
        {
            get { return gender; }
            set
            {
                if (gender == value)
                {
                    return;
                }
                gender = value;
                OnPropertyChanged("Gender");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
