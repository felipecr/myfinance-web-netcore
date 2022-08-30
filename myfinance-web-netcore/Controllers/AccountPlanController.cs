using Microsoft.AspNetCore.Mvc;
using myfinance_web_netcore.Domain;
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
            List<AccountPlanModel> accountPlans = new AccountPlan().getAccountPlans();
            ViewBag.List = accountPlans;
            return View();
        }

        [HttpGet]
        public IActionResult CreateAccountPlan(int? id)
        {
            if (id != null)
            {
                AccountPlanModel accountPlan = new AccountPlan().GetAccountPlanById(id);
                ViewBag.AccountPlan = accountPlan;
            }

            ViewBag.AccountPlanTypes = new AccountPlanType().getAccountPlanTypes();
            return View();
        }

        [HttpPost]
        public IActionResult CreateAccountPlan(AccountPlanModel form)
        {
            AccountPlan accountPlan = new AccountPlan();

            if (form.Id == null)
            {
                accountPlan.Insert(form);
            }
            else
            {
                accountPlan.Update(form);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult DeleteAccountPlan(int id)
        {
            new AccountPlan().Delete(id);
            return RedirectToAction("Index");
        }
    }
}