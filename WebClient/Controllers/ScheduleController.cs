using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using WebClient.Models;

namespace WebClient.Controllers
{
    public class ScheduleController : Controller
    {
        private readonly string _apiKey = "?x-api-key=069D25CA8A5241688DBB7DA09FE68D75";
        Uri baseAddress = new Uri("https://localhost:7224/api");
        [HttpGet]
        public IActionResult Index(string sortOrder, string searchString)
        {
            List<ScheduleViewModel> scheduleList = new List<ScheduleViewModel>();
            //Request
            using (var http = new HttpClient())
            {
                var response = http.GetAsync(baseAddress + "/Schedules/GetSchedules" + _apiKey).Result;
                if (response.IsSuccessStatusCode)
                {
                    var body = response.Content.ReadAsStringAsync().Result;
                    scheduleList = JsonConvert.DeserializeObject<List<ScheduleViewModel>>(body);
                }

                ViewData["TrainSortParm"] = String.IsNullOrEmpty(sortOrder) ? "Train" : "";
                ViewData["StationSortParm"] = String.IsNullOrEmpty(sortOrder) ? "Station" : "";
                ViewData["IdSortParm"] = String.IsNullOrEmpty(sortOrder) ? "id" : "";
                ViewData["CurrentFilter"] = searchString;
                if (!String.IsNullOrEmpty(searchString))
                {
                    scheduleList = scheduleList.Where(t => t.StationId == int.Parse(searchString)).ToList();
                }

                switch (sortOrder)
                {
                    case "name":
                        scheduleList = scheduleList.OrderBy(t => t.TrainId).ToList();
                        break;
                    case "platformCount":
                        scheduleList = scheduleList.OrderBy(t => t.StationId).ToList();
                        break;
                    default:
                        scheduleList = scheduleList.OrderBy(t => t.Id).ToList();
                        break;
                }
                return View(scheduleList);
            }
        }

        // https://localhost:7049/api/gettrain?id=2
        [HttpGet("Schedule/Get")]
        public IActionResult Get(int id)
        {
            using (var http = new HttpClient())
            {
                var response = http.GetAsync(baseAddress + "/Schedules/GetSchedule/" + id + _apiKey).Result;
                if (response.IsSuccessStatusCode)
                {
                    var body = response.Content.ReadAsStringAsync().Result;
                    return View(JsonConvert.DeserializeObject<ScheduleViewModel>(body));
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
        public IActionResult Create(ScheduleViewModel schedule)
        {
            try
            {
                string data = JsonConvert.SerializeObject(schedule);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                using (var http = new HttpClient())
                {
                    HttpResponseMessage response = http.PostAsync(baseAddress + "/Schedules/PostSchedule" + _apiKey, content).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        TempData["successMessage"] = "Schedule Added.";
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
                    ScheduleViewModel schedule = new ScheduleViewModel();
                    HttpResponseMessage response = http.GetAsync(baseAddress + "/Schedules/GetSchedule/" + id + _apiKey).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        string data = response.Content.ReadAsStringAsync().Result;
                        schedule = JsonConvert.DeserializeObject<ScheduleViewModel>(data);
                    }
                    return View(schedule);
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }
        [HttpPost]
        public IActionResult Edit(ScheduleViewModel schedule)
        {
            try
            {
                using (var http = new HttpClient())
                {
                    string data = JsonConvert.SerializeObject(schedule);
                    StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = http.PutAsync(baseAddress + "/Schedules/PutSchedule/" + schedule.Id + _apiKey, content).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        TempData["successMessage"] = "Schedule updated successfully.";
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
                ScheduleViewModel station = new ScheduleViewModel();
                using (var http = new HttpClient())
                {
                    HttpResponseMessage response = http.GetAsync(baseAddress + "/Schedules/GetSchedule/" + id + _apiKey).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        string data = response.Content.ReadAsStringAsync().Result;
                        station = JsonConvert.DeserializeObject<ScheduleViewModel>(data);
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
                    HttpResponseMessage response = http.DeleteAsync(baseAddress + "/Schedules/DeleteSchedule/" + id + _apiKey).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        TempData["successMessage"] = "Schedule deleted successfully.";
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

