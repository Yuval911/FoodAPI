using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using FoodModel;

namespace FoodClient
{
    class Program
    {
        private const string URL = "http://localhost:50811/";

        static void Main(string[] args)
        {
            HttpClient client = new HttpClient
            {
                BaseAddress = new Uri(URL)
            };

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync("api/food").Result;
            if (response.IsSuccessStatusCode)
            {
                var dataObjects = response.Content.ReadAsAsync<IEnumerable<Food>>().Result;

                foreach (var d in dataObjects)
                {
                    Console.WriteLine(d);
                }
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }

            Console.Write("\n Add new food? (y/n): ");

            if (Console.ReadLine() != "y")
            {
                client.Dispose();
                return;
            }

            Food food = new Food();

            Console.Write("Food name: ");
            food.Name = Console.ReadLine();

            Console.Write("Food calories: ");
            food.Calories = Convert.ToInt32(Console.ReadLine());

            Console.Write("Food ingridients: ");
            food.Ingridients = Console.ReadLine();

            Console.Write("Food grade: ");
            food.Grade = Convert.ToInt32(Console.ReadLine());

            HttpClient clientPost = new HttpClient()
            {
                BaseAddress = new Uri(URL),
            };

            clientPost.DefaultRequestHeaders.Accept.Clear();
            clientPost.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var responsePost = clientPost.PostAsJsonAsync("api/food", food).Result;

            Console.WriteLine(responsePost);

            client.Dispose();

            Console.ReadLine();
        }
    }
}
