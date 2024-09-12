using Newtonsoft.Json;
using System.Text;
public class ApiProvider
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;

    public ApiProvider(string baseUrl)
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri(baseUrl);
        _baseUrl = baseUrl;
    }

    // POST metodu
    public async Task<O> POST<I, O>(I data, string endpoint, string token)
    {
        try
        {
            // Veriyi JSON formatına dönüştürme
            string json = JsonConvert.SerializeObject(data);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }

            // POST isteğini gönderme
            HttpResponseMessage response = await _httpClient.PostAsync(endpoint, content);
            response.EnsureSuccessStatusCode(); // Başarı kontrolü

            // Yanıtı okuma ve deserileştirme
            string responseData = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<O>(responseData);
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine("Request error: " + e.Message);
            throw;
        }
    }
}
