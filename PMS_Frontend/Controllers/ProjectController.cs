using System.Text;
using System.Text.Json;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using PMS_Frontend.Models.ViewModels.ProjectVM;
using PMS_Frontend.Models.ViewModels.UserVM;
using Shared.ApiUri;

namespace PMS_Frontend.Controllers
{
    public class ProjectController : Controller
    {
        readonly HttpClient _httpClient;

        public ProjectController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public IActionResult Project()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject(DtoProjectUI dto)
        {
            if (dto != null && dto.Members != null)
            {
                string GetUseruri = $"{ApiUrls.UserUrl}/getuserbynickname";
                string CheckMembersUri = $"{ApiUrls.UserUrl}/checkmembers";
                var userNickname = new DtoUserNickName
                {
                    NickName = HttpContext.Session.GetString("UserNickName") ?? string.Empty
                };
                var checknickname = new DtoCheckMembers
                {
                    Nickname = dto.Members.Select(m => m.MemberName).ToList()
                };

                var usercontent = new StringContent(JsonSerializer.Serialize(userNickname), Encoding.UTF8, "application/json");

                var memberscontent = new StringContent(JsonSerializer.Serialize(checknickname), Encoding.UTF8, "application/json");

                var userresponse = await _httpClient.PostAsync(GetUseruri, usercontent);

                var membersresponse = await _httpClient.PostAsync(CheckMembersUri, memberscontent);

                if (userresponse.IsSuccessStatusCode && membersresponse.IsSuccessStatusCode)
                {
                    string userresult = await userresponse.Content.ReadAsStringAsync();
                    var userResponse = JsonSerializer.Deserialize<Guid>(userresult, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                    var projectresult = await membersresponse.Content.ReadFromJsonAsync<UserCheckResultDto>();

                    DtoPostProject postDto = new()
                    {
                        OwnerId = userResponse,
                        ProjectDescription = dto.ProjectDescription,
                        ProjectName = dto.ProjectName,
                        Members = dto.Members.Select(m => new DtoPostMember
                        {
                            UserId = projectresult.UserIds.ContainsKey(m.MemberName) ? projectresult.UserIds[m.MemberName] : Guid.Empty,
                            Role = m.Role
                        }).ToList()
                    };
                    string CreateProjecturi = $"{ApiUrls.ProjectUrl}/create";
                    var projectcontent = new StringContent(JsonSerializer.Serialize(postDto), Encoding.UTF8, "application/json");
                    Console.WriteLine(projectcontent.Headers);
                    var projectresponse = await _httpClient.PostAsync(CreateProjecturi, projectcontent);
                    return Ok(dto);
                }
                else
                {
                    return View("Error");
                }
            }
            else
            {
                return View("Error");
            }
        }

        public async Task<IActionResult> ProjectView()
        {
            //Bu Kısmı Metodlaştırabilirsin Sonradan //MessageControllerdada Aynı Yer Mevcut

            string GetUseruri = $"{ApiUrls.UserUrl}/getuserbynickname";
            string GetUsersNickName = $"{ApiUrls.UserUrl}/getusersnickname";
            var userNickname = new DtoUserNickName
            {
                NickName = HttpContext.Session.GetString("UserNickName") ?? string.Empty
            };
            var usercontent = new StringContent(JsonSerializer.Serialize(userNickname), Encoding.UTF8, "application/json");
            var userresponse = await _httpClient.PostAsync(GetUseruri, usercontent);
            string userresult = await userresponse.Content.ReadAsStringAsync();
            var userid = JsonSerializer.Deserialize<Guid>(userresult, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            //  
            DtoGetProject project = new()
            {
                Id = userid
            };
            var content = new StringContent(JsonSerializer.Serialize(project), Encoding.UTF8, "application/json");
            string GetProjecturi = $"{ApiUrls.ProjectUrl}/getproject";
            var response = await _httpClient.PostAsync(GetProjecturi, content);
            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Projeler yüklenirken hata oluştu.";
                return View(new List<DtoProjectUI>());
            }

            string projectResult = await response.Content.ReadAsStringAsync();
            var projects = JsonSerializer.Deserialize<List<DtoGetProjectContent>>(projectResult, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            var userIds = projects
      .Select(p => p.OwnerId)
      .Concat(projects.SelectMany(p => p.Members.Select(m => m.UserId)))
      .Distinct()
      .ToList();


            var userIdsContent = new StringContent(JsonSerializer.Serialize(userIds), Encoding.UTF8, "application/json");
            var nicknamesResponse = await _httpClient.PostAsync(GetUsersNickName, userIdsContent);

            string result = await nicknamesResponse.Content.ReadAsStringAsync();

            var nicknames = JsonSerializer.Deserialize<List<DtoGetNickNames>>(result, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }); 
            var nicknameDictionary = nicknames?.ToDictionary(u => u.Id, u => u.NickName) ?? new Dictionary<Guid, string>();

            ViewBag.NickNames = nicknameDictionary;
            return View(projects);

        }
    }
}
