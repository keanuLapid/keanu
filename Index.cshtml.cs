using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace KeanuLapid.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        [BindProperty]
        public SpacecraftSearchResult? DataResult { get; set; }

        public async Task<IActionResult> OnGet()
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.GetAsync("https://stapi.co/api/v2/rest/spacecraft/search");

            if (response.IsSuccessStatusCode)
            {
                string? content = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(content))
                {
                    SpacecraftSearchResult? result = JsonConvert.DeserializeObject<SpacecraftSearchResult>(content);

                    if (result != null)
                    {
                        this.DataResult = result;
                    }
                }
            }

            return Page();
        }

        public class SpacecraftSearchResult
        {
            [JsonProperty("page")]
            public PageInfo Page { get; set; } = new PageInfo();

            [JsonProperty("sort")]
            public SortInfo Sort { get; set; } = new SortInfo();

            [JsonProperty("spacecrafts")]
            public List<Spacecraft> Spacecrafts { get; set; } = new List<Spacecraft>();
        }

        public class PageInfo
        {
            [JsonProperty("pageNumber")]
            public int PageNumber { get; set; }

            [JsonProperty("pageSize")]
            public int PageSize { get; set; }

            [JsonProperty("numberOfElements")]
            public int NumberOfElements { get; set; }

            [JsonProperty("totalElements")]
            public int TotalElements { get; set; }

            [JsonProperty("totalPages")]
            public int TotalPages { get; set; }

            [JsonProperty("firstPage")]
            public bool FirstPage { get; set; }

            [JsonProperty("lastPage")]
            public bool LastPage { get; set; }
        }

        public class SortInfo
        {
            [JsonProperty("clauses")]
            public List<object> Clauses { get; set; } = new List<object>();
        }

        public class Spacecraft
        {
            [JsonProperty("uid")]
            public string Uid { get; set; } = string.Empty;

            [JsonProperty("name")]
            public string? Name { get; set; }

            [JsonProperty("registry")]
            public string? Registry { get; set; }

            [JsonProperty("status")]
            public string? Status { get; set; }

            [JsonProperty("dateStatus")]
            public string? DateStatus { get; set; }

            [JsonProperty("spacecraftClass")]
            public SpacecraftClass? SpacecraftClass { get; set; }
        }

        public class SpacecraftClass
        {                                                                       
            [JsonProperty("uid")]
            public string Uid { get; set; } = string.Empty;

            [JsonProperty("name")]
            public string? Name { get; set; }
        }
    }
}
