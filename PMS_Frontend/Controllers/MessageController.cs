using System.Text.Json;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using PMS_Frontend.Models.ViewModels.MessageVM;
using Shared.ApiUri;
using PMS_Frontend.Models.ViewModels.UserVM;

namespace PMS_Frontend.Controllers
{
    public class MessageController : Controller
    {
        readonly HttpClient _httpClient;

        public MessageController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Message()
        {
            //TODO: Metotlaşma
            string GetUseruri = $"{ApiUrls.UserUrl}/getuserbynickname";
            var userNickname = new DtoUserNickName
            {
                NickName = HttpContext.Session.GetString("UserNickName") ?? string.Empty
            };
            var userContent = new StringContent(JsonSerializer.Serialize(userNickname), Encoding.UTF8, "application/json");
            var userResponse = await _httpClient.PostAsync(GetUseruri, userContent);
            var userResult = await userResponse.Content.ReadAsStringAsync();
            var userId = JsonSerializer.Deserialize<Guid>(userResult, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            var response = await _httpClient.GetAsync($"{ApiUrls.MessageUrl}/getmessage?id={userId}");
            var result = await response.Content.ReadAsStringAsync();
            var messages = JsonSerializer.Deserialize<List<MessageListVM>>(result, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            var senderIds = messages.Select(m => m.SenderId).Distinct().ToList();

            var nicknameRequestContent = new StringContent(JsonSerializer.Serialize(senderIds), Encoding.UTF8, "application/json");
            var nicknameResponse = await _httpClient.PostAsync($"{ApiUrls.UserUrl}/getusersnickname", nicknameRequestContent);
            var nicknameJson = await nicknameResponse.Content.ReadAsStringAsync();

            var nicknameList = JsonSerializer.Deserialize<List<DtoGetNickNames>>(nicknameJson, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            var nicknameDict = nicknameList.ToDictionary(n => n.Id, n => n.NickName);

            MessagePageVM messagePage = new()
            {
                PreviousMessages = messages.Select(m => new MessageListVM
                {
                    Content = m.Content,
                    Timestamp = m.Timestamp,
                    SenderNickName = nicknameDict.TryGetValue(m.SenderId, out var nick) ? nick : "Bilinmiyor"
                }).ToList()
            };

            return View(messagePage);
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(MessagePageVM message)
        {
            string GetUseruri = $"{ApiUrls.UserUrl}/getuserbynickname";
            var userNickname = new DtoUserNickName
            {
                NickName = HttpContext.Session.GetString("UserNickName") ?? string.Empty
            };
            var userContent = new StringContent(JsonSerializer.Serialize(userNickname), Encoding.UTF8, "application/json");
            var userResponse = await _httpClient.PostAsync(GetUseruri, userContent);
            var userResult = await userResponse.Content.ReadAsStringAsync();
            var userId = JsonSerializer.Deserialize<Guid>(userResult, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            DtoUserNickName nickName = new()
            {
                NickName = message.NewMessage.ReceiverNickName
            };
            //Burasıda Birden Fazla Yerde Kullanıldı 

            var nicknameRequestContent = new StringContent(JsonSerializer.Serialize(nickName), Encoding.UTF8, "application/json");
            var nicknameResponse = await _httpClient.PostAsync($"{ApiUrls.UserUrl}/getuserbynickname", nicknameRequestContent);
            var nicknameJson = await nicknameResponse.Content.ReadAsStringAsync();

            var receiverId = JsonSerializer.Deserialize<Guid>(nicknameJson, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            MessageCreateDto createMessage = new()
            {
                Content = message.NewMessage.Content,
                SenderId = userId,
                ReceiverId = receiverId
            };

            var messageRequestContent = new StringContent(JsonSerializer.Serialize(createMessage), Encoding.UTF8, "application/json");

            await _httpClient.PostAsync($"{ApiUrls.MessageUrl}/create", messageRequestContent);
        
            HttpContext.Session.SetString("LastMessageRecipient", message.NewMessage.ReceiverNickName);
            return RedirectToAction("Message");
        }
    }
}
