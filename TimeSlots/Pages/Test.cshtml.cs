using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using SQLitePCL;
using System.Text;
using TimeSlots.Model;
using TimeSlots.Queries;

namespace TimeSlots.Pages
{
    public class TestModel : PageModel
    {
        [BindProperty] public string Date { get; set; }
        [BindProperty] public int Pallets { get; set; }

        public List<TimeslotDto> Dtos { get; set; } = new();
        public Dictionary<DateTime, List<TimeslotDto>> Timeslots { get; set; } = new();

		public void OnGet()
        {
        }

        public async Task OnPost()
        {
            var query = new GetTimeslotsQuery(DateTime.Parse(Date), Pallets);
            using var client = new HttpClient();
            var requestBody = JsonConvert.SerializeObject(query);

            using var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7027/gettimeslots")
            {
                Content = new StringContent(requestBody, Encoding.UTF8, "application/json")
            };

            using var response = await client.SendAsync(request);

			if (response.IsSuccessStatusCode)
            {
                var dtos = await response.Content.ReadAsStringAsync();

				Dtos = JsonConvert.DeserializeObject<List<TimeslotDto>>(dtos);
                
                foreach(var day in Dtos.Select(d => d.Date).Distinct())
                {
                    Timeslots.Add(day, Dtos.Where(d => d.Date == day).ToList());
                }
            }
        }
    }
}
