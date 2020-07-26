using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EmailTemplate.Web.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using MimeKit;

namespace EmailTemplate.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _env;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment env)
        {
            _logger = logger;
            _env = env;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Email()
        {
            var pathToFile = _env.WebRootPath + Path.DirectorySeparatorChar.ToString()
                            + "Template" + Path.DirectorySeparatorChar.ToString() + "email-template.html";
            var subject = "Confirm Account Registration";

            var builder = new BodyBuilder();
            using (StreamReader SourceReader = System.IO.File.OpenText(pathToFile))
            {
                builder.HtmlBody = SourceReader.ReadToEnd();
            }
            //{0} : Subject  
            //{1} : DateTime  
            //{2} : Email  
            //{3} : Username  
            //{4} : Password   

            string messageBody = string.Format(builder.HtmlBody,
                subject,
                String.Format("{0:dddd, d MMMM yyyy}", DateTime.Now),
                "nazmul@test.com",
                "Nazmul",
                "123456");
            var model = new EmailModel
            {
                HtmlData = messageBody
            };
            return View(model);
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
