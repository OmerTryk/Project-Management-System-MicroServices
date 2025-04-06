using System.Diagnostics;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc;
using PMS_Frontend.Models;
using PMS_Frontend.Models.ViewModels.UserVM;
using Shared.ApiUri;

namespace PMS_Frontend.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        readonly HttpClient _httpClient;

        public HomeController(ILogger<HomeController> logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            string? nickname = HttpContext.Session.GetString("UserNickName") ?? string.Empty; ;
            ViewBag.UserNickName = HttpContext.Session.GetString("UserNickName");
            var notifications = await _httpClient.GetFromJsonAsync<List<DtoNotification>>($"{ApiUrls.NotificationUrl}/notifications/{nickname}");

            return View(notifications);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
