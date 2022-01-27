using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataModels.DataModels;
using DatabaseEF.Models;

namespace DatabaseEF.Controllers
{
    [Route("[controller]/[action]")]
    public class EmployeeLearningController : Controller
    {
        private readonly IEmployeeLearningRepository employeeLearningRepository;

        public EmployeeLearningController(IEmployeeLearningRepository employeeLearningRepository)
        {
            this.employeeLearningRepository = employeeLearningRepository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLearnings(int id)
        {
            ViewBag.EmployeeId = id;
            Console.WriteLine(ViewBag.EmployeeId);
            var result = await employeeLearningRepository.GetEmployeeLearningsAsync(id);
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> CreateLearning(int id)
        {
            ViewBag.EmployeeId = id.ToString();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateLearning(EmployeeLearning model)
        {
            if(ModelState.IsValid)
            {
                await employeeLearningRepository.CreateEmployeeLearningAsync(model);
                return RedirectToAction("getlearnings", new { Id = model.EmployeeId});
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditLearning(int id)
        {
            EmployeeLearning model = await employeeLearningRepository.ViewEmployeeLearningAsync(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditLearning(EmployeeLearning model)
        {
            if(ModelState.IsValid)
            {
                await employeeLearningRepository.EditEmployeeLearningAsync(model);
                return RedirectToAction("getlearnings", new { Id = model.EmployeeId });
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> DetailsLearning(int id)
        {
            var model = await employeeLearningRepository.ViewEmployeeLearningAsync(id);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteLearning(int id)
        {
            var model = await employeeLearningRepository.DeleteEmployeeLearningAsync(id);
            return RedirectToAction("getlearnings", new { Id = model.EmployeeId});
        }
    }
}
