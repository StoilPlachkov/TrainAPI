using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json.Serialization;
using WebClient.Models;

namespace WebClient.Controllers
{
    public class StationController : Controller
    {
        private readonly string _apiKey = "?x-api-key=069D25CA8A5241688DBB7DA09FE68D75";
        Uri baseAddress = new Uri("https://localhost:7224/api");
        [HttpGet]
        public IActionResult Index(string sortOrder, string searchString)
        {
            List<StationViewModel> stationList = new List<StationViewModel>();
            //Request
            using (var http = new HttpClient())
            {
                var response = http.GetAsync(baseAddress + "/Stations/GetStations" + _apiKey).Result;
                if (response.IsSuccessStatusCode)
                {
                    var body = response.Content.ReadAsStringAsync().Result;
                    stationList = JsonConvert.DeserializeObject<List<StationViewModel>>(body);
                }

                ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name" : "";
                ViewData["PlatformSortParm"] = String.IsNullOrEmpty(sortOrder) ? "platformCount" : "";
                ViewData["OperationalSortParm"] = String.IsNullOrEmpty(sortOrder) ? "operational" : "";
                ViewData["IdSortParm"] = String.IsNullOrEmpty(sortOrder) ? "id" : "";
                ViewData["CurrentFilter"] = searchString;
                if (!String.IsNullOrEmpty(searchString))
                {
                    stationList = stationList.Where(t => t.Name.Contains(searchString)).ToList();
                }

                switch (sortOrder)
                {
                    case "name":
                        stationList = stationList.OrderBy(t => t.Name).ToList();
                        break;
                    case "platformCount":
                        stationList = stationList.OrderBy(t => t.PlatformCount).ToList();
                        break;
                    case "operational":
                        stationList = stationList.OrderBy(t => t.IsOperational).ToList();
                        break;
                    case "id":
                        stationList = stationList.OrderBy(t => t.Id).ToList();
                        break;
                    default:
                        stationList = stationList.OrderBy(t => t.Id).ToList();
                        break;
                }
                return View(stationList);
            }
        }

        // https://localhost:7049/api/gettrain?id=2
        [HttpGet("Station/Get")]
        public IActionResult Get(int id)
        {
            using (var http = new HttpClient())
            {
                var response = http.GetAsync(baseAddress + "/Stations/GetStation/" + id + _apiKey).Result;
                if (response.IsSuccessStatusCode)
                {
                    var body = response.Content.ReadAsStringAsync().Result;
                    return View(JsonConvert.DeserializeObject<StationViewModel>(body));
                }
                return View();
            }
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(StationViewModel station)
        {
            try
            {
                string data = JsonConvert.SerializeObject(station);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                using (var http = new HttpClient())
                {
                    HttpResponseMessage response = http.PostAsync(baseAddress + "/Stations/PostStation" + _apiKey, content).Result;


                    if (response.IsSuccessStatusCode)
                    {
                        TempData["successMessage"] = "Station Added.";
                        return RedirectToAction("Index");
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            return View();
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                using (var http = new HttpClient())
                {
                    StationViewModel station = new StationViewModel();
                    HttpResponseMessage response = http.GetAsync(baseAddress + "/Stations/GetStation/" + id + _apiKey).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        string data = response.Content.ReadAsStringAsync().Result;
                        station = JsonConvert.DeserializeObject<StationViewModel>(data);
                    }
                    return View(station);
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }
        [HttpPost]
        public IActionResult Edit(StationViewModel station)
        {
            try
            {
                using (var http = new HttpClient())
                {
                    string data = JsonConvert.SerializeObject(station);
                    StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = http.PutAsync(baseAddress + "/Stations/PutStation/" + station.Id + _apiKey, content).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        TempData["successMessage"] = "Station updated successfully.";
                        return RedirectToAction("Index");
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            return View();
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                StationViewModel station = new StationViewModel();
                using (var http = new HttpClient())
                {
                    HttpResponseMessage response = http.GetAsync(baseAddress + "/Stations/GetStation/" + id + _apiKey).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        string data = response.Content.ReadAsStringAsync().Result;
                        station = JsonConvert.DeserializeObject<StationViewModel>(data);
                    }
                    return View(station);
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirm(int id)
        {
            try
            {
                using (var http = new HttpClient())
                {
                    HttpResponseMessage response = http.DeleteAsync(baseAddress + "/Stations/DeleteStation/" + id + _apiKey).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        TempData["successMessage"] = "Station deleted successfully.";
                        return RedirectToAction("Index");
                    }

                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            return View();
        }
    }
}
