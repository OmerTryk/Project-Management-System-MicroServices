using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using PMS_Frontend.Models.ViewModels.UserVM;

namespace PMS_Frontend.Controllers
{
    public class UserController : Controller
    {
        readonly HttpClient _httpClient;

        public UserController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(DtoRegister dto)
        {
            string apiUrl = "https://localhost:7202/api/user/create";
            var jsonContent = new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(apiUrl, jsonContent);
            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                return RedirectToAction("Login");
            }
            else
            {
                return View("Error");
            }
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(DtoLogin dtoLogin)
        {
            string apiUrl = "https://localhost:7202/api/user/login";
            var jsonContent = new StringContent(JsonSerializer.Serialize(dtoLogin), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(apiUrl, jsonContent);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<TokenResponse>();

                if (result != null)
                {
                    HttpContext.Session.SetString("JwtToken", result.Token);
                    HttpContext.Session.SetString("UserNickName", dtoLogin.NickName);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Error = "Token alınırken bir hata oluştu.";
                    return View(dtoLogin);
                }
            }
            else
            {
                ViewBag.Error = "Geçersiz kullanıcı adı veya şifre!";
                return View(dtoLogin);
            }

        }
    }
}
