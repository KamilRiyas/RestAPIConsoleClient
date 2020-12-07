using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

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

        static async Task GetAllItemsAsync()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/todos/");
                HttpResponseMessage response = await client.GetAsync(client.BaseAddress);
                if (response.IsSuccessStatusCode)
                {
                    List<todoItem> itemsList = new List<todoItem>();
                    itemsList = await JsonSerializer.DeserializeAsync<List<todoItem>>(await response.Content.ReadAsStreamAsync());
                    foreach(var item in itemsList)
                    {
                        Console.WriteLine($"Item Id    : {item.id}");
                        Console.WriteLine($"User Id    : {item.userId}");
                        Console.WriteLine($"Title      : {item.title}");
                        Console.WriteLine(item.completed ? "Status     :Completed" : "Status     :Pending");
                    }
                }

            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("What do you want to do?");
            Console.WriteLine("1. Get one item");
            Console.WriteLine("2. Get all items");
            int option = Convert.ToInt32(Console.ReadLine());
            switch (option)
            {
                case 1:
                    int itemId = 0;
                    Console.WriteLine("Please enter the Id to be retieved");
                    itemId = Convert.ToInt32(Console.ReadLine());
                    GetItemAsync(itemId).Wait();
                    break;
                case 2:
                    GetAllItemsAsync().Wait();
                    break;
                default:
                    break;
            }

            Console.ReadLine();
        }
    }
}