using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DormitoryManagementSystem.API.Controllers;
public class Accounts : Controller
{
    // GET: Accounts
    public ActionResult Index()
    {
        return View();
    }

    // GET: Accounts/Details/5
    public ActionResult Details(int id)
    {
        return View();
    }

    // GET: Accounts/Create
    public ActionResult Create()
    {
        return View();
    }

    // POST: Accounts/Create
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

    // GET: Accounts/Edit/5
    public ActionResult Edit(int id)
    {
        return View();
    }

    // POST: Accounts/Edit/5
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

    // GET: Accounts/Delete/5
    public ActionResult Delete(int id)
    {
        return View();
    }

    // POST: Accounts/Delete/5
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
