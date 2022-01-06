using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration; //IConfiguration
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using BooksApi.Models;

namespace BooksMVC.Controllers
{
    public class ClientController : Controller
    {
        private readonly HttpClient client;
        private readonly string WebApiPath;
        private readonly IConfiguration _configuration;

        public ClientController(IConfiguration configuration)
        {
            _configuration = configuration;
            WebApiPath = _configuration["BooksApiConfig:Url"];   //read from appsettings.json
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("ApiKey", _configuration["BooksApiConfig:ApiKey"]);   //use on any http calls      
        }

        // GET: ClientController
        public async Task<ActionResult> Index()
        {
            List<BooksItem> books = null;
            HttpResponseMessage response = await client.GetAsync(WebApiPath);
            if (response.IsSuccessStatusCode)
            {
                books = await response.Content.ReadAsAsync<List<BooksItem>>();  //requires System.Net.Http.Formatting.Extension
            }
            return View(books);
        }

        // GET: ClientController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            HttpResponseMessage response = await client.GetAsync(WebApiPath + id);
            if (response.IsSuccessStatusCode)
            {
                BooksItem book = await response.Content.ReadAsAsync<BooksItem>();
                return View(book);
            }
            return NotFound();
        }

        // GET: ClientController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClientController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]  //todo add deoc to slides
        public async Task<ActionResult> Create([Bind("Id,Title,Author,Genre,IsAvailable")] BooksItem book)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(WebApiPath, book);
                response.EnsureSuccessStatusCode();
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: ClientController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            HttpResponseMessage response = await client.GetAsync(WebApiPath + id);
            if (response.IsSuccessStatusCode)
            {
                BooksItem book = await response.Content.ReadAsAsync<BooksItem>();
                return View(book);
            }
            return NotFound();
        }


        // POST: ClientController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [Bind("Id,Title,Author,Genre,IsAvailable")] BooksItem book)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage response = await client.PutAsJsonAsync(WebApiPath + id, book);
                response.EnsureSuccessStatusCode();
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: ClientController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            HttpResponseMessage response = await client.GetAsync(WebApiPath + id);
            if (response.IsSuccessStatusCode)
            {
                BooksItem book = await response.Content.ReadAsAsync<BooksItem>();
                return View(book);
            }
            return NotFound();
        }


        // POST: ClientController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, int notUsed = 0)
        {
            HttpResponseMessage response = await client.DeleteAsync(WebApiPath + id);
            response.EnsureSuccessStatusCode();
            return RedirectToAction(nameof(Index));
        }
    }
}

