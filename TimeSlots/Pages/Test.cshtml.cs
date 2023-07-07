using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;
using TimeSlots.Model;
using TimeSlots.Model.Enums;
using TimeSlots.Queries;

namespace TimeSlots.Pages
{
	public class TestModel : PageModel
	{
		[BindProperty] public string Date { get; set; }
		[BindProperty] public int Pallets { get; set; }
		[BindProperty] public TaskType TaskType { get; set; }

		[BindProperty] public List<Guid> SelectedTimeslotIds { get; set; }

		public Dictionary<DateTime, List<TimeslotDto>> Timeslots { get; set; } = new();
		private readonly ILogger<TestModel> _logger;

		public TestModel(ILogger<TestModel> logger)
		{
			_logger = logger;
		}


		public void OnGet()
		{
		}

		public async Task OnPost()
		{
			var companyId = Guid.Parse("f13e9549-9b98-46fb-b696-a990432e2710");
			var query = new GetTimeslotsQuery(DateTime.Parse(Date), Pallets, TaskType, companyId);
			
			var dtos = await GetTimeslotDtosAsync(query);
			//Response.Cookies.Append("Timeslots", JsonConvert.SerializeObject(dtos));
			HttpContext.Session.SetString("Timeslots", JsonConvert.SerializeObject(dtos));
			foreach (var day in dtos.Select(d => d.Date).Distinct())
			{
				Timeslots.Add(day, dtos.Where(d => d.Date == day).ToList());
			}

		}

		private async Task<List<TimeslotDto>> GetTimeslotDtosAsync(GetTimeslotsQuery query)
		{
			using var client = new HttpClient();
			var requestBody = JsonConvert.SerializeObject(query);
			using var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7027/gettimeslots")
			{
				Content = new StringContent(requestBody, Encoding.UTF8, "application/json")
			};
			using var response = await client.SendAsync(request);

			if (response.IsSuccessStatusCode)
			{
				var responseString = await response.Content.ReadAsStringAsync();
				return JsonConvert.DeserializeObject<List<TimeslotDto>>(responseString);
			}
			else
			{
				return new List<TimeslotDto> { };
			}
		}

		public async Task<IActionResult> OnPostReserveAsync()
		{
			if(HttpContext.Session.TryGetValue("Timeslots", out byte[] timeslotsData))
			{
				var timeslotsString = Encoding.UTF8.GetString(timeslotsData);
				var allTimeslots = JsonConvert.DeserializeObject<List<TimeslotDto>>(timeslotsString);
				var dtos = allTimeslots.Where(d => SelectedTimeslotIds.Contains(d.Id)).ToList();

				using var client = new HttpClient();
				

				foreach (var dto in dtos)
				{
					var requestBody = JsonConvert.SerializeObject(dto);
					using var message = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7027/settimeslots")
					{
						Content = new StringContent(requestBody, Encoding.UTF8, "application/json")
					};

					using var responseMessage = await client.SendAsync(message);

					if (responseMessage.IsSuccessStatusCode)
					{
						_logger.LogInformation($"Reserved timeslot for date {dto.Date}");
					}
					else
					{
						_logger.LogError(responseMessage.StatusCode.ToString());
					}
				}
			}
			

			return RedirectToPage();
		}
	}
}
