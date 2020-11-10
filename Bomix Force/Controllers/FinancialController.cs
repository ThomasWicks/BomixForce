using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bomix_Force.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bomix_Force.Controllers
{
    public class FinancialController : Controller
    {
        // GET: FinancialController
        public ActionResult Index()
        {
            List<FinancialViewModel> financialViewModel = new List<FinancialViewModel>();
            return View(financialViewModel);
        }

        // GET: FinancialController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: FinancialController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FinancialController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FinancialController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FinancialController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FinancialController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FinancialController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
