using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WPFTest.Client.Model;

namespace WPFTest.Client.Service
{
    public class HttpService
    {
        private readonly HttpClient _client;

        private readonly string _url = "https://localhost:5001/api/"; 

        public HttpService()
        {
            _client = new HttpClient();
        }

        public IEnumerable<Person> GetPeopleForTable(int take, int skip, int lang, string search)
        {
            List<Person> people = null;

            var url = $"{_url}people/GetPeopleForTable?take={take}&skip={skip}&lang={lang}";

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
    }
}
