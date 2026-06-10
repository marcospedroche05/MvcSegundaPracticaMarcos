using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace MvcSegundaPracticaMarcos.Services
{
    public class ServiceIA
    {
        private string ApiUrl;
        private MediaTypeWithQualityHeaderValue header;

        public ServiceIA(IConfiguration configuration)
        {
            this.ApiUrl = configuration.GetValue<string>("ApiUrls:ApiPreguntas");
            this.header = new MediaTypeWithQualityHeaderValue("application/json");
        }

        public async Task<string> PreguntarAsync(string pregunta)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(this.header);

                var body = JsonConvert.SerializeObject(new { pregunta = pregunta });
                StringContent content = new StringContent(body, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(this.ApiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    dynamic result = JsonConvert.DeserializeObject<dynamic>(json);
                    return result.respuesta;
                }
                else
                {
                    return "No se pudo obtener respuesta de la IA.";
                }
            }
        }
    }
}