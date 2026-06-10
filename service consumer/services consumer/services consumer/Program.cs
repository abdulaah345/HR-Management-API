using services_consumer.Model;
using System.Net.Http.Json;

namespace services_consumer
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            HttpClient httpClient = new HttpClient();
           Dept d= await httpClient.GetFromJsonAsync<Dept>("http://localhost:5177/api/Department/1");
            Console.WriteLine(d.name);
        }
    }
}
