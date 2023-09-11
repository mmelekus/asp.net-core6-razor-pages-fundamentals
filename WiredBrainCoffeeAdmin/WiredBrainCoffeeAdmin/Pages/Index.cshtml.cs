using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using WiredBrainCoffeeAdmin.Data.Models;

namespace WiredBrainCoffeeAdmin.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IHttpClientFactory _clientFactory;

        public List<SurveyItem> SurveyResults { get; set; }

        public IDictionary<string, string> OrderStats { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IHttpClientFactory factory)
        {
            _logger = logger;
            _clientFactory = factory;
        }

        public async void OnGet()
        {
            var client = _clientFactory.CreateClient();

            var response = await client.GetAsync("https://wiredbraincoffeeadmin.azurewebsites.net/api/orderStats");
            var responseData = await response.Content.ReadAsStringAsync();
            OrderStats = JsonSerializer.Deserialize<IDictionary<string, string>>(responseData);

            var rawJson = System.IO.File.ReadAllText("wwwroot/sampledata/survey.json");
            SurveyResults = JsonSerializer.Deserialize<List<SurveyItem>>(rawJson);
        }
    }
}