using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace RestAPIConsoleClient
{
    class todoItem
    {
        public int id { get; set; }
        public int userId { get; set; }
        public string title { get; set; }
        public bool completed { get; set; }
    }
    class Program
    {
        static async Task GetItemAsync(int itemId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/todos/" + itemId);
                HttpResponseMessage response = await client.GetAsync(client.BaseAddress);
                if (response.IsSuccessStatusCode)
                {
                    todoItem item = new todoItem();
                    item = await JsonSerializer.DeserializeAsync<todoItem>(await response.Content.ReadAsStreamAsync());
                    Console.WriteLine($"Item Id    : {item.id}");
                    Console.WriteLine($"User Id    : {item.userId}");
                    Console.WriteLine($"Title      : {item.title}");
                    Console.WriteLine(item.completed ? "Completed" : "Pending");
                }

            }
        }
        static void Main(string[] args)
        {
            int itemId = 0;
            Console.WriteLine("Please enter the Id to be retieved");
            itemId = Convert.ToInt32(Console.ReadLine());
            GetItemAsync(itemId).Wait();
            Console.ReadLine();
        }
    }
}