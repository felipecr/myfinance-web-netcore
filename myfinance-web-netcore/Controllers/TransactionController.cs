using Microsoft.AspNetCore.Mvc;
using myfinance_web_netcore.Domain;
using myfinance_web_netcore.Models;

namespace myfinance_web_netcore.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ILogger<TransactionController> _logger;

        public TransactionController(ILogger<TransactionController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<TransactionModel> transactions = new Transaction().getTransactions();
            ViewBag.List = transactions;
            return View();
        }

        [HttpGet]
        public IActionResult TransactionsReport()
        {
            TransactionsReportModel model = new TransactionsReportModel();
            return View(model);
        }

         [HttpPost]
        public IActionResult TransactionsReport(TransactionsReportModel model)
        {
            if (model.StartDate != null || model.EndDate != null) {
                model.Transactions = new Transaction().filterTransactions(model.StartDate, model.EndDate);
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult CreateTransaction(int? id)
        {
            if (id != null)
            {
                TransactionModel transaction = new Transaction().GetTransactionById(id);
                ViewBag.Transaction = transaction;
            }
  
            ViewBag.AccountPlans = new AccountPlan().getAccountPlans();
            return View();
        }

        [HttpPost]
        public IActionResult CreateTransaction(TransactionModel form)
        {
            Transaction transaction = new Transaction();

            if (!String.IsNullOrEmpty(form.MaskedValue)) {
                String value = form.MaskedValue.Replace("R$", string.Empty);
                value.Replace(" ", string.Empty);
                value.Replace(".", string.Empty);

                form.Value = decimal.Parse(value);
            }

             if (form.Id == null)
            {
                transaction.Insert(form);
            }
            else
            {
                transaction.Update(form);
            }
            
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult DeleteTransaction(int id)
        {
            new Transaction().Delete(id);
            return RedirectToAction("Index");
        }
    }
}