using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using stackoverflow_tags.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace stackoverflow_tags.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Home_Index()
        {
            decimal count_sum = 0;
            RootObject tag = new RootObject();
            List<RootObject> tagList = new List<RootObject>();
            using (var httpClient = new HttpClient())
            {


                    for (int i = 1; i <= 10; i++)
                    {
                        var bytes = await httpClient.GetByteArrayAsync("https://api.stackexchange.com/2.3/tags?page=" + i + "&pagesize=100&order=desc&sort=popular&site=stackoverflow");
                        var decompressedJson = new StreamReader(new GZipStream(new MemoryStream(bytes), CompressionMode.Decompress)).ReadToEnd();

                        tag = JsonConvert.DeserializeObject<RootObject>(decompressedJson);
                    
                        tagList.Add(tag);
                    }
                    foreach (var items in tagList)
                    {
                        foreach (var item in items.items)
                        {
                            count_sum = count_sum + item.count;
                        }
                    }

                    foreach (var items in tagList)
                    {
                        foreach (var item in items.items)
                        {
                        item.procent = ((item.count * 100) / count_sum);
                        }
                    }




            }
           
            return View(tagList);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
