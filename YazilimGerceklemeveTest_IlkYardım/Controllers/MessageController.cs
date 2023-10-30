using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using YazilimGerceklemeveTest_IlkYardım.Models;

namespace YazilimGerceklemeveTest_IlkYardım.Controllers
{
    public class MessageController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(MessageViewModel message)
        {

            string apiKey = "sec_vU7W3Hont9Ex7FClzMUqDa8qkNEGCHfx";
            string sourceId = "cha_WKrXbJm0yJYmAL7QdynZU";
            string url = "https://api.chatpdf.com/v1/chats/message";

            var data = new
            {
                stream = true,
                sourceId = sourceId,
                messages = new[]
                {
                new
                {
                    role = "user",
                    content = message.RequestMessage
                }
            }
            };


            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("x-api-key", apiKey);

                var jsonContent = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

                try
                {
                    using (var response = await client.PostAsync(url, jsonContent))
                    {
                        response.EnsureSuccessStatusCode();

                        using (var responseStream = await response.Content.ReadAsStreamAsync())
                        using (var reader = new StreamReader(responseStream))
                        {
                            char[] buffer = new char[1024];
                            int bytesRead;

                            while ((bytesRead = await reader.ReadAsync(buffer, 0, buffer.Length)) > 0)
                            {
                                string chunk = new string(buffer, 0, bytesRead);
                                ViewBag.Message = chunk;
                                return View();
                                //return View("Chunk: " + chunk.Trim());
                            }
                        }
                    }
                }
                catch (HttpRequestException e)
                {
                    return View("Error: " + e.Message);
                }
            }
            return View();
        }
    }
}
