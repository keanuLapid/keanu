using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KeanuLapid.Pages
{
    public class ErrorModel : PageModel
    {
        public bool ShowRequestId { get; set; }
        public string? RequestId { get; set; }

        public void OnGet()
        {
            RequestId = HttpContext.TraceIdentifier;
            ShowRequestId = !string.IsNullOrEmpty(RequestId);
        }
    }
}
