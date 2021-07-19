using AppAspCoreMvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace AppAspCoreMvc.Controllers
{
    public class StudentController : Controller
    {
        private readonly HttpClient httpClient;
        private string urlBase;
       
        public StudentController(HttpClient httpClient, IConfiguration configuration)
        {
            this.httpClient = httpClient;
            this.urlBase = configuration.GetValue<string>("UrlApi");
        }

        public async Task<IActionResult> Index(int? page=1)//string sortOrder, string currentFilter, string searchString,
        {
            
            List<Student> studentList = new List<Student>();
            var request = await httpClient.GetAsync($"{urlBase}/Students");
            string responseApi = await request.Content.ReadAsStringAsync();
            studentList = JsonConvert.DeserializeObject<List<Student>>(responseApi);
        
            int pageNumber = (page ?? 1);
            var onePageOfStudents = studentList.ToPagedList(pageNumber, 12);
            ViewBag.PageList = onePageOfStudents;
            return View(onePageOfStudents);
        }
        /// <summary>
        /// GET View
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Create Resource POST
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Create(Student student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(student), Encoding.UTF8, "application/json");
                    var response = await httpClient.PostAsync($"{urlBase}/Students", content);
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        TempData["message"] = "Item created!";
                        return RedirectToAction("Index");
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.ToString();
                throw;
            }

            return View();
        }

        /// <summary>
        /// GET View
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Edit(int ? id)
        {
            if(id == 0)
            {
                return NotFound();
            }

            Student st = new Student();
            var response = await httpClient.GetAsync($"{urlBase}/Students/{id}");
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string responseApi = await response.Content.ReadAsStringAsync();
                st = JsonConvert.DeserializeObject<Student>(responseApi);
                return View(st);
            }
            else
            {
                ViewBag.StatusCode = response.StatusCode;
            }

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int id, Student student)
        {
            if (id == 0 || student == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(student), Encoding.UTF8, "application/json");
                var response = await httpClient.PutAsync($"{urlBase}/Students/{id}", content);
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    TempData["message"] = "Item updated!";
                    return RedirectToAction("Index");
                }
            }
            return View();
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id==0)
            {
                return NotFound();
            }

            var response = await httpClient.DeleteAsync($"{urlBase}/Students/{id}");
            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                TempData["message"] = "Item deleted!";
                return RedirectToAction("Index");
            }
                
            return View();
        }

        /// <summary>
        /// GET View
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Assign(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            Student st = new Student();
            var response = await httpClient.GetAsync($"{urlBase}/Students/{id}");
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string responseApi = await response.Content.ReadAsStringAsync();
                st = JsonConvert.DeserializeObject<Student>(responseApi);
                return View(st);
            }
            else
            {
                ViewBag.StatusCode = response.StatusCode;
            }
                
            return View();
        }
    }
}
