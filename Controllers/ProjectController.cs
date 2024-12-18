using Microsoft.AspNetCore.Mvc;
using System.Net;
using Web.Models;
using Web.Utils.Services;

namespace Web.Controllers
{
    public class ProjectController : Controller
    {
        private readonly ApiService apiService;
        public ProjectController(ApiService apiService)
        {
            this.apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
               ViewBag.ErrorMessage = "";
               var result = await apiService.GetAllAsync<ProjectViewModel>("Projects");

                if (result.StatusCode != HttpStatusCode.OK)
                {
                    ViewBag.ErrorMessage = "Projects error";
                    return View(new List<ProjectViewModel>());
                }

                return View((List<ProjectViewModel>)result.Data);
            }
            catch (Exception)
            {
                ViewBag.ErrorMessage = "Projects error";
                return View(new List<ProjectViewModel>());
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id, Name, Description, StartDate, EndDate")] ProjectViewModel project)
        {
            if (ModelState.IsValid)
            {
                var result = await apiService.CreateAsync<ProjectViewModel>("Projects", project);

                if (result != HttpStatusCode.Created)
                {
                    ViewBag.ErrorMessage = "Projects error";
                    return View(project);
                }

                return RedirectToAction(nameof(Index));
            }

            return View(project);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var result = await apiService.GetAsync<ProjectViewModel>($"Projects/{id}");

            if (result.StatusCode != HttpStatusCode.OK)
            {
                ViewBag.ErrorMessage = "Projects error";
                return RedirectToAction(nameof(Index));
            }

            return View(result.Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id, Name, Description, StartDate, EndDate")] ProjectViewModel project)
        {
            if (ModelState.IsValid)
            {
                var result = await apiService.UpdateAsync("Projects", project);

                if (result != HttpStatusCode.OK)
                {
                    return BadRequest();
                }

                return RedirectToAction(nameof(Index));
            }

            return View(project);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await apiService.DeleteAsync($"Projects/{id}");

                if (result != HttpStatusCode.OK)
                {
                    ViewBag.ErrorMessage = "Projects error";
                    return View(new List<ProjectViewModel>());
                }

                return RedirectToAction(nameof(Index));   
            }
            catch (Exception)
            {
                ViewBag.ErrorMessage = "Projects error";
                return View(new List<ProjectViewModel>());
            }
        }
    }
}
