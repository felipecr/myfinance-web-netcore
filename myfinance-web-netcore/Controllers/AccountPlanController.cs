using Microsoft.AspNetCore.Mvc;
using myfinance_web_netcore.Models;

namespace myfinance_web_netcore.Controllers
{
    public class AccountPlanController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public AccountPlanController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<AccountPlanModel> accountPlans = new AccountPlanModel().getAccountPlans();
            ViewBag.List = accountPlans;
            return View();
        }

        [HttpGet]
        public IActionResult CreateAccountPlan(int? id)
        {
            if (id != null)
            {
                AccountPlanModel accountPlan = new AccountPlanModel().GetAccountPlanById(id);
                ViewBag.AccountPlan = accountPlan;
            }

            return View();
        }

        [HttpPost]
        public IActionResult CreateAccountPlan(AccountPlanModel form)
        {
            if (form.Id == null)
            {
                form.Insert();
            }
            else
            {
                form.Update(form.Id);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult DeleteAccountPlan(int id)
        {
            new AccountPlanModel().Delete(id);
            return RedirectToAction("Index");
        }
    }
}