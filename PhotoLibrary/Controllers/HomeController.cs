using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using PhotoLibrary.Models;
using PhotoLibrary.Models.Services;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Reflection;

namespace PhotoLibrary.Controllers
{
    public class HomeController : Controller
    {
        private PhotoServices photoServices;
        string Baseurl = "https://jsonplaceholder.typicode.com/"; //baslänken till API

        public HomeController(PhotoServices photoServices)
        {
            this.photoServices = photoServices; // dependecy injection av photoservices i konstruktorn
        }
    
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Index()
        {
            List<Album> albums = new List<Album>(); //skapar en lista av alla Album

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl); 

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync("albums");

                if (Res.IsSuccessStatusCode) //om API anropet är successful 
                {
                    var AlbumResponse = Res.Content.ReadAsStringAsync().Result;

                    albums = JsonConvert.DeserializeObject<List<Album>>(AlbumResponse);

                }
                                            //else vi hade kunna ta datan från API och spara i vår databas ifall APIn funkade inte 
                
                var model = this.photoServices.BuildAlbumVM(albums);
                return View(model);

            }

        }


        [HttpGet]
        [Route("/details{UserId}")]
        public async Task<IActionResult> Details(int userId)
        {
            List<Photo> photos = new List<Photo>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync("photos");

                if (Res.IsSuccessStatusCode)
                {
                    var PhotoResponse = Res.Content.ReadAsStringAsync().Result;

                    photos = JsonConvert.DeserializeObject<List<Photo>>(PhotoResponse);

                }
                var model = this.photoServices.GetRightAlbum(userId, photos);
                return View(model);

            }


        }

    }
}