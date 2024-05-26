using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json.Serialization;
using WebClient.Models;

namespace WebClient.Controllers
{
    public class TrainController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7224/api");
        [HttpGet]
        public IActionResult Index()
        {
            List<TrainViewModel> trainLits = new List<TrainViewModel>();
            //Request
            using (var http = new HttpClient())
            {
                var response = http.GetAsync(baseAddress + "/Trains/GetTrains").Result;
                if (response.IsSuccessStatusCode)
                {
                    var body = response.Content.ReadAsStringAsync().Result;
                    trainLits = JsonConvert.DeserializeObject<List<TrainViewModel>>(body);
                }
                return View(trainLits);
            }
        }

        // https://localhost:7049/api/gettrain?id=2
        [HttpGet("Train/Get")]
        public IActionResult Get(int id)
        {
            using (var http = new HttpClient())
            {
                var response = http.GetAsync(baseAddress + "/Trains/GetTrain/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    var body = response.Content.ReadAsStringAsync().Result;
                    return View(JsonConvert.DeserializeObject<TrainViewModel>(body));
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
        public IActionResult Create(TrainViewModel train)
        {
            try
            {
                string data = JsonConvert.SerializeObject(train);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                using (var http = new HttpClient())
                {
                    HttpResponseMessage response = http.PostAsync(baseAddress + "/Trains/PostTrain", content).Result;


                    if (response.IsSuccessStatusCode)
                    {
                        TempData["successMessage"] = "Train Added.";
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
                    TrainViewModel train = new TrainViewModel();
                    HttpResponseMessage response = http.GetAsync(baseAddress + "/Trains/GetTrain/" + id).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        string data = response.Content.ReadAsStringAsync().Result;
                        train = JsonConvert.DeserializeObject<TrainViewModel>(data);
                    }
                    return View(train);
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }
        [HttpPost]
        public IActionResult Edit(TrainViewModel train)
        {
            try
            {
                using (var http = new HttpClient())
                {
                    string data = JsonConvert.SerializeObject(train);
                    StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = http.PutAsync(baseAddress + "/Trains/PutTrain/" + train.Id, content).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        TempData["successMessage"] = "Train updated successfully.";
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
