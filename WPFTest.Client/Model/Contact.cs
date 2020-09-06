using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using WPFTest.Client.Enums;

namespace WPFTest.Client.Model
{
    public class Contact : INotifyPropertyChanged, IDataErrorInfo
    {
        private ContactType contactType;
        private string contactValue;

        public string this[string columnName]
        {
            get
            {
                Error = string.Empty;
                switch (columnName)
                {
                    case "ContactValue":
                        if (ContactType == ContactType.Email)
                        {
                            Regex regex = new Regex(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
                            bool isValid = regex.IsMatch(ContactValue.Trim());
                            if (!isValid)
                            {
                                Error = "Invalid Email.";
                            } 
                        }
                        if (ContactType == ContactType.Phone)
                        {
                            Regex regex = new Regex(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$");
                            bool isValid = regex.IsMatch(ContactValue.Trim());
                            if (!isValid)
                            {
                                Error = "Invalid Phone.";
                            } 
                        }
                        break;
                    default:
                        break;
                }
                return Error;
            }
        }

        public ContactType ContactType 
        {
            get { return contactType; }
            set
            {
                if (contactType == value)
                {
                    return;
                }
                contactType = value;
                OnPropertyChanged("ContactType");
            } 
        }

        public string ContactValue 
        {
            get { return contactValue; }
            set
            {
                if (contactValue == value)
                {
                    return;
                }
                contactValue = value;
                OnPropertyChanged("ContactValue");
            }
        }

        public int PersonId { get; set; }

        public int PersonContactId { get; set; }

        public string Note { get; set; }

        public byte Active { get; set; }

        public string Error { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
