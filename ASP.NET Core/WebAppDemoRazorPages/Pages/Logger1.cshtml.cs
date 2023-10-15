using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using WebAppDemoRazorPages.Models;

namespace WebAppDemoRazorPages.Pages
{
    public class Logger1Model : PageModel
    {
        private readonly string logPath;
        public IList<LoggerIndex> Log { get; set; }
        public Logger1Model(IConfiguration configuration) 
        {
            logPath = configuration.GetValue<string>("AppConfiguration:LogPath");
            Log= JsonSerializer.Deserialize<IList<LoggerIndex>>(System.IO.File.ReadAllText(logPath));
        }
        public void OnGet()
        {
        }
    }

}
