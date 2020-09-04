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
                string error = string.Empty;
                switch (columnName)
                {
                    case "Value":
                        if (ContactType == ContactType.Email)
                        {
                            Regex regex = new Regex(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
                            bool isValid = regex.IsMatch(Value.Trim());
                            if (!isValid)
                            {
                                error = "Invalid Email.";
                            } 
                        }
                        break;
                    default:
                        break;
                }
                return error;
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

        public string Value 
        {
            get { return contactValue; }
            set
            {
                if (contactValue == value)
                {
                    return;
                }
                contactValue = value;
                OnPropertyChanged("Value");
            }
        }

        public int PersonId { get; set; }

        public int PersonContactId { get; set; }

        public string Note { get; set; }

        public byte Active { get; set; }

        public string Error => null;

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
