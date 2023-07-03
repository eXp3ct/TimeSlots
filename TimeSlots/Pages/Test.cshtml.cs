using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;
using TimeSlots.Model;
using TimeSlots.Queries;

namespace TimeSlots.Pages
{
	public class TestModel : PageModel
	{
		[BindProperty] public string Date { get; set; }
		[BindProperty] public int Pallets { get; set; }

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
				var dtosResponse = await response.Content.ReadAsStringAsync();

				var dtos = JsonConvert.DeserializeObject<List<TimeslotDto>>(dtosResponse);

				foreach (var day in dtos.Select(d => d.Date).Distinct())
				{
					Timeslots.Add(day, dtos.Where(d => d.Date == day).ToList());
				}
			}
		}
	}
}
