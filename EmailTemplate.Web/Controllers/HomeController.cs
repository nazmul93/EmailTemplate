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
using EmailTemplate.Web.Data;

namespace EmailTemplate.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _env;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment env,
            ApplicationDbContext context)
        {
            _logger = logger;
            _env = env;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Email(int id)
        {
            Template template = null;
            if (id > 0)
                template = _context.Template.FirstOrDefault(x => x.Id == id);
            else
                template = _context.Template.FirstOrDefault();
            
            var heading = "Confirm Account Registration";
            var date = String.Format("{0:dddd, d MMMM yyyy}", DateTime.Now);
            
            var builder = new BodyBuilder
            {
                HtmlBody = template.Body
            };
            var contact = _context.Contacts.FirstOrDefault(x => x.Id == 1);
            var name = contact.Name;

            string messageBody = string.Format(builder.HtmlBody,
                heading, date, name);

            var model = new EmailModel
            {
                Subject = template.Subject,
                HtmlData = messageBody
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Email(EmailModel email)
        {
            string messageBody = "";
            if (ModelState.IsValid)
            {
                //var builder = new BodyBuilder
                //{
                //    HtmlBody = email.HtmlData
                //};

                //messageBody = string.Format(builder.HtmlBody,
                //String.Format("{0:dddd, d MMMM yyyy}", DateTime.Now),
                //"nazmul@test.com",
                //"Nazmul",
                //"123456");
            }
            var model = new EmailModel
            {
                HtmlData = messageBody
            };
            return View(model);
        }

        public IActionResult Template()
        {
            var pathToFile = _env.WebRootPath + Path.DirectorySeparatorChar.ToString()
                            + "Template" + Path.DirectorySeparatorChar.ToString() + "email-template1.html";
            
            var builder = new BodyBuilder();
            using (StreamReader SourceReader = System.IO.File.OpenText(pathToFile))
            {
                builder.HtmlBody = SourceReader.ReadToEnd();
            }

            var model = new EmailModel
            {
                HtmlData = string.Format(builder.HtmlBody)
            };
            return View(model);
        }

        public IActionResult GetTemplate(int id)    
        {
            Template template = null;
            if (id > 0)
                template = _context.Template.FirstOrDefault(x => x.Id == id);
            else
                template = _context.Template.FirstOrDefault();
            var model = new EmailModel
            {
                Subject = template.Subject,
                HtmlData = template.Body
            };
            return View("Template", model);
        }

        [HttpPost]
        public IActionResult Template(EmailModel email)
        {
            string messageBody = "";
            if (ModelState.IsValid)
            {
                var template = new Template
                {
                    Id = 0,
                    Subject = email.Subject,
                    Body = email.HtmlData,
                    CreatedAt = DateTime.Now
                };

                try
                {
                    _context.Template.Add(template);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    messageBody = template.Body;
                }
            }
            var model = new EmailModel
            {
                HtmlData = messageBody
            };
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
