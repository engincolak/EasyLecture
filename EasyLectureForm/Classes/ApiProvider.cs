using System;
using System.Net.Http;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class ApiProvider
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;

    public ApiProvider(string baseUrl)
    {
        _httpClient = new HttpClient();
        _baseUrl = baseUrl;
    }

    // POST metodu
    public async Task<O> POST<I, O>(I data, string endpoint, string token)
    {
        try
        {
            string json = JSonConvert.Desialize<I>(data);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync(endpoint, content);
            response.EnsureSuccessStatusCode(); // Başarı kontrolü

            string responseData = await response.Content.ReadAsStringAsync();

            return JSonConvert.Serialize<O>(responseData);
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine("Request error: " + e.Message);
            throw;
        }
    }



    // GET metodu
    public async Task<T> GET<T>(string endpoint)
    {
        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(new Uri(new Uri(_baseUrl), endpoint));
            response.EnsureSuccessStatusCode(); // Başarı kontrolü

            string responseData = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(responseData);
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine("Request error: " + e.Message);
            throw;
        }
    }

    // DELETE metodu
    public async Task DELETE(string endpoint)
    {
        try
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync(new Uri(new Uri(_baseUrl), endpoint));
            response.EnsureSuccessStatusCode(); // Başarı kontrolü
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine("Request error: " + e.Message);
            throw;
        }
    }

    // PUT metodu
    public async Task<T> PUT<T>(string endpoint, object data)
    {
        try
        {
            string json = JsonSerializer.Serialize(data);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PutAsync(new Uri(new Uri(_baseUrl), endpoint), content);
            response.EnsureSuccessStatusCode(); // Başarı kontrolü

            string responseData = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(responseData);
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine("Request error: " + e.Message);
            throw;
        }
    }
}
