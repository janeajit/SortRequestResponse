using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace SortRequestResponse
{
    public class Posts
    {
        public string id { get; set; }
        public string body { get; set; }
        public int wordcount { get; set; }
    }

    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            var httpClient = new HttpClient();
            List<Posts> postsList = new List<Posts>();

            var json = await httpClient.GetStringAsync("https://jsonplaceholder.typicode.com/posts");
            var jArray = JArray.Parse(json);
            foreach (JObject item in jArray)
            {
                Posts p = new Posts();
                p.body = item.GetValue("body").ToString();
                p.id = item.GetValue("id").ToString();
                string[] words = p.body.Split(' ', '\n');
                p.wordcount = words.Length;
                postsList.Add(p);
            }
            List<Posts> sortedList = postsList.OrderByDescending(o => o.wordcount).ToList();
            foreach (var item in sortedList)
            {
                Console.WriteLine("Post ID is {0} and Word count is {1}", item.id, item.wordcount);
            }
            Console.WriteLine("Done");
        }
    }
}
