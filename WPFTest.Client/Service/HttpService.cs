using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
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

        public async Task<PeopleWithCount> GetPeopleForTable(int take, int skip, int lang, string search)
        {
            PeopleWithCount people = null;

            var url = $"{_url}{_people}/GetPeopleForTable?take={take}&skip={skip}&lang={lang}";

            if (!string.IsNullOrEmpty(search))
            {
                url += $"&search={search}";
            }

            var request = await _client.GetAsync(url);

            if (request.IsSuccessStatusCode)
            {
                var data = await request.Content.ReadAsStringAsync();
                people = JsonConvert.DeserializeObject<PeopleWithCount>(data);
            }

            return people;
        }

        public bool DeletePeople(List<int> ids)
        {
            var url = $"{_url}{_people}/deletepeople";

            var myContent = JsonConvert.SerializeObject(ids);
            var buffer = Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var request = _client.PostAsync(url, byteContent).Result;
            if (request.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public bool SavePerson(Person person)
        {
            var url = $"{_url}{_people}";

            if (person.AddressNo > 0)
            {
                url += $"/{person.AddressNo}";
                var myContent = JsonConvert.SerializeObject(new
                {
                    Id = person.AddressNo,
                    Title = person.Title,
                    Fname = person.FirstName,
                    Lname = person.LastName,
                    Cpny = person.Company,
                    Street = person.Street,
                    CountryCode = person.Country.CountryCode,
                    Zip = person.PostalCode,
                    City = person.City,
                    GenderId = (int)person.Gender,
                    GreetingId = person.Greeting.Id,
                    Notes = string.Empty
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
                    Title = person.Title,
                    Fname = person.FirstName,
                    Lname = person.LastName,
                    Cpny = person.Company,
                    Street = person.Street,
                    CountryCode = person.Country.CountryCode,
                    Zip = person.PostalCode,
                    City = person.City,
                    GenderId = (int)person.Gender,
                    GreetingId = person.Greeting.Id,
                    Notes = string.Empty
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

        public List<Contact> GetContactsForPerson(int personId)
        {
            List<Contact> contacts = null;

            var url = $"{_url}{_personContact}/GetPersonContactsByPersonId/{personId}";

            var request = _client.GetAsync(url).Result;

            if (request.IsSuccessStatusCode)
            {
                var data = request.Content.ReadAsStringAsync().Result;
                contacts = JsonConvert.DeserializeObject<IEnumerable<dynamic>>(data).Select(e=> new Contact() {
                    PersonId = e.personId,
                    PersonContactId = e.personContactId,
                    ContactType = (ContactType)e.contactTypeId,
                    ContactValue = e.txt,
                    Note = e.notes,
                    Active = e.active
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
                    PersonId = result.personId,
                    PersonContactId = result.personContactId,
                    ContactType = (ContactType)result.contactTypeId,
                    ContactValue = result.txt,
                    Note = result.notes,
                    Active = result.active
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
                    Txt = contact.ContactValue,
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
                    Txt = contact.ContactValue,
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

        public bool DeleteContacts(int personId, List<int> ids)
        {
            var url = $"{_url}{_personContact}/deletecontacts/{personId}";

            var myContent = JsonConvert.SerializeObject(ids);
            var buffer = Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var request = _client.PostAsync(url, byteContent).Result;
            if (request.IsSuccessStatusCode)
            {
                return true;
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
