using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Runtime.CompilerServices;
using Taller1.Shared.Models;

namespace Taller1.Server.Controllers
{
    [Route("api/characters")]
    public class CharactersController : Controller
    {
        private HttpClient _httpClient;
        public CharactersController()
        {
            _httpClient = new HttpClient();
        }
        [HttpGet]
        [Route("all")]
        public async Task<Characters> GetCharacters(int page, string name = null)
        {
            try
            {
                Characters characters = null;
                string url = $"https://rickandmortyapi.com/api/character?page={page}";

                if (!string.IsNullOrEmpty(name))
                {
                    url += $"&name={name}";
                }

                HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync(url);
                httpResponseMessage.EnsureSuccessStatusCode();
                string responseBody = await httpResponseMessage.Content.ReadAsStringAsync();
                characters = JsonConvert.DeserializeObject<Characters>(responseBody);
                return characters;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
