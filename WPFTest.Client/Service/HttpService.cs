using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using WPFTest.Client.Enums;
using WPFTest.Client.Model;

namespace WPFTest.Client.Service
{
    public class HttpService
    {
        private readonly HttpClient _client;

        private const string _url = "https://localhost:5001/api/";
        private const string _people = "people";
        private const string _countries = "countries";
        private const string _greetings = "greetings";
        private const string _personContact = "personcontact";

        public HttpService()
        {
            _client = new HttpClient();
        }

        public List<Person> GetPeopleForTable(int take, int skip, int lang, string search)
        {
            List<Person> people = null;

            var url = $"{_url}{_people}/GetPeopleForTable?take={take}&skip={skip}&lang={lang}";

            if (!string.IsNullOrEmpty(search))
            {
                url += $"&search={search}";
            }

            var request = _client.GetAsync(url).Result;

            if (request.IsSuccessStatusCode)
            {
                var data = request.Content.ReadAsStringAsync().Result;
                people = JsonConvert.DeserializeObject<List<Person>>(data);
            }

            return people;
        }

        public Person GetPerson(int id)
        {
            Person person = null;

            var url = $"{_url}{_people}/{id}";

            var request = _client.GetAsync(url).Result;

            if (request.IsSuccessStatusCode)
            {
                var data = request.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<dynamic>(data);
                person = new Person()
                {
                    AddressNo = result.Id,
                    Birthday = result.DateOfBirth.HasValue ? result.DateOfBirth.Value.ToString() : string.Empty,
                    Street = result.Street,
                    City = result.City,
                    Company = result.Cpny,
                    FirstName = result.Fname,
                    LastName = result.Lname,
                    PostalCode = result.Zip,
                    Registration = result.FirstContact.ToString(),
                    Title = result.Title,
                    GreetingId = result.GreetingId,
                    CountryCode = result.CountryCode,
                    Gender = (Gender)result.GenderId
                };
            }

            return person;
        }

        public List<Contact> GetContactsForPerson(int personId)
        {
            List<Contact> contacts = null;

            var url = $"{_url}{_personContact}/GetPersonContactsByPersonId/{personId}";

            var request = _client.GetAsync(url).Result;

            if (request.IsSuccessStatusCode)
            {
                var data = request.Content.ReadAsStringAsync().Result;
                contacts = JsonConvert.DeserializeObject<IEnumerable<dynamic>>(data).Select(e=> new Contact() {
                    PersonId = e.PersonId,
                    PersonContactId = e.PersonContactId,
                    ContactType = (ContactType)e.ContactTypeId,
                    Value = e.Txt,
                    Note = e.Notes,
                    Active = e.Active
                }).ToList();
            }

            return contacts;
        }

        public Contact GetContact(int contactId, int personId)
        {
            Contact contact = null;

            var url = $"{_url}{_personContact}/{contactId}/{personId}";

            var request = _client.GetAsync(url).Result;

            if (request.IsSuccessStatusCode)
            {
                var data = request.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<dynamic>(data);
                contact = new Contact()
                {
                    PersonId = result.PersonId,
                    PersonContactId = result.PersonContactId,
                    ContactType = (ContactType)result.ContactTypeId,
                    Value = result.Txt,
                    Note = result.Notes,
                    Active = result.Active
                };
            }

            return contact;
        }

        public bool SaveContact(Contact contact)
        {
            var url = $"{_url}{_personContact}";

            if (contact.PersonContactId > 0)
            {
                url += $"/{contact.PersonContactId}/{contact.PersonId}";
                var myContent = JsonConvert.SerializeObject(new 
                {
                    PersonId = contact.PersonId,
                    PersonContactId = contact.PersonContactId,
                    ContactTypeId = (int)contact.ContactType,
                    Txt = contact.Value,
                    Notes = contact.Note,
                    Active = contact.Active
                });
                var buffer = Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var request = _client.PutAsync(url, byteContent).Result;
                if (request.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            else
            {
                var myContent = JsonConvert.SerializeObject(new
                {
                    PersonId = contact.PersonId,
                    PersonContactId = 0,
                    ContactTypeId = (int)contact.ContactType,
                    Txt = contact.Value,
                    Notes = string.Empty,
                    Active = 1
                });
                var buffer = Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var request = _client.PostAsync(url, byteContent).Result;
                if (request.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            return false;
        }

        public List<Country> GetCountries(int lang)
        {
            List<Country> countries = null;

            var url = $"{_url}{_countries}";

            var request = _client.GetAsync(url).Result;

            if (request.IsSuccessStatusCode)
            {
                var data = request.Content.ReadAsStringAsync().Result;
                countries = JsonConvert.DeserializeObject<IEnumerable<dynamic>>(data).Select(e => new Country()
                {
                    CountryCode = e.code,
                    CountryName = CheckLocalization(lang, e)
                }).ToList();
            }

            return countries;
        }

        public List<Greeting> GetGreetings(int lang)
        {
            List<Greeting> greetings = null;

            var url = $"{_url}{_greetings}";

            var request = _client.GetAsync(url).Result;

            if (request.IsSuccessStatusCode)
            {
                var data = request.Content.ReadAsStringAsync().Result;
                greetings = JsonConvert.DeserializeObject<IEnumerable<dynamic>>(data).Select(e => new Greeting()
                {
                    Id = e.id,
                    Text = CheckLocalization(lang, e)
                }).ToList();
            }

            return greetings;
        }

        private string CheckLocalization(int lang, dynamic entity)
        {
            string result;
            switch (lang)
            {
                case 1:
                    result = entity.txt1;
                    break;
                case 2:
                    result = entity.txt2;
                    break;
                case 3:
                    result = entity.txt3;
                    break;
                case 4:
                    result = entity.txt4;
                    break;
                default:
                    result = entity.txt1;
                    break;
            }

            if (string.IsNullOrEmpty(result))
            {
                result = entity.txt1;
            }

            return result;
        }
    }
}
