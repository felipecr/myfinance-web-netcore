using System.Data;
using System.Text;
using myfinance_web_netcore.Infra;
using myfinance_web_netcore.Models;

namespace myfinance_web_netcore.Domain
{
    public class Transaction
    {
        public void Insert(TransactionModel form)
        {
            DAL dalInstance = DAL.GetInstance;
            dalInstance.Connect();

            string sql = "INSERT INTO TRANSACTIONS(DATE, VALUE, DESCRIPTION, ACCOUNT_PLAN_ID) " + 
                $"VALUES ('{form.Date.ToString("yyyy-MM-dd")}'," +
                $"{form.Value}, '{form.Description}', {form.AccountPlanId})";

            dalInstance.ExecuteCommand(sql);
            dalInstance.Disconnect();
        }

        public void Update(TransactionModel form)
        {
            DAL dalInstance = DAL.GetInstance;
            dalInstance.Connect();

            string sql = $"UPDATE TRANSACTIONS SET DATE = '{form.Date.ToString("yyyy-MM-dd")}'," +
                $"VALUE = {form.Value}, DESCRIPTION = '{form.Description}', " +
                $"ACCOUNT_PLAN_ID = {form.AccountPlanId} WHERE ID = {form.Id}";

            dalInstance.ExecuteCommand(sql);
            dalInstance.Disconnect();
        }

        public void Delete(int id)
        {
            DAL dal = DAL.GetInstance;
            dal.Connect();
            string sql = $"DELETE FROM TRANSACTIONS WHERE ID = {id}";
            dal.ExecuteCommand(sql);
            dal.Disconnect();
        }

        public TransactionModel GetTransactionById(int? id)
        {
            DAL dalInstance = DAL.GetInstance;
            dalInstance.Connect();

            string sql = $"SELECT ID, DATE, VALUE, DESCRIPTION, ACCOUNT_PLAN_ID FROM TRANSACTIONS WHERE ID = '{id}'";
            DataTable dataTable = dalInstance.Select(sql);

            TransactionModel transaction = new TransactionModel()
            {
                Id = int.Parse(dataTable.Rows[0]["ID"].ToString()),
                Date = DateTime.Parse(dataTable.Rows[0]["DATE"].ToString()),
                Value = decimal.Parse(dataTable.Rows[0]["VALUE"].ToString()),
                Description = dataTable.Rows[0]["DESCRIPTION"].ToString(),
                AccountPlanId = int.Parse(dataTable.Rows[0]["ACCOUNT_PLAN_ID"].ToString())
            };

            dalInstance.Disconnect();

            return transaction;
        }

        public List<TransactionModel> getTransactions()
        {
            List<TransactionModel> transactions = new List<TransactionModel>();
            DAL dalInstance = DAL.GetInstance;
            dalInstance.Connect();

            StringBuilder sql = new StringBuilder("SELECT t.ID, t.DATE, t.VALUE, t.DESCRIPTION,");
            sql.Append(" ap.DESCRIPTION ACCOUNT_PLAN_DESCRIPTION, apt.DESCRIPTION ACCOUNT_PLAN_TYPE_DESCRIPTION");
            sql.Append(" FROM TRANSACTIONS t");
            sql.Append(" INNER JOIN ACCOUNT_PLANS ap on t.ACCOUNT_PLAN_ID = ap.ID");
            sql.Append(" INNER JOIN ACCOUNT_PLAN_TYPES apt on ap.ACCOUNT_PLAN_TYPE_ID = apt.ID");

            DataTable dataTable = dalInstance.Select(sql.ToString());

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                TransactionModel transaction = new TransactionModel()
                {
                    Id = int.Parse(dataTable.Rows[i]["ID"].ToString()),
                    Date = DateTime.Parse(dataTable.Rows[i]["DATE"].ToString()),
                    Value = decimal.Parse(dataTable.Rows[i]["VALUE"].ToString()),
                    Description = dataTable.Rows[i]["DESCRIPTION"].ToString(),
                    AccountPlanDescription = dataTable.Rows[i]["ACCOUNT_PLAN_DESCRIPTION"].ToString(),
                    AccountPlanTypeDescription = dataTable.Rows[i]["ACCOUNT_PLAN_TYPE_DESCRIPTION"].ToString()
                };

                transactions.Add(transaction);
            }

            dalInstance.Disconnect();

            return transactions;
        }

        public List<TransactionModel> filterTransactions(DateTime? startDate, DateTime? endDate)
        {
            List<TransactionModel> transactions = new List<TransactionModel>();
            DAL dalInstance = DAL.GetInstance;
            dalInstance.Connect();

            StringBuilder sql = new StringBuilder("SELECT tr.ID, tr.DATE, tr.VALUE, tr.DESCRIPTION,");
            sql.Append(" ap.DESCRIPTION ACCOUNT_PLAN_DESCRIPTION, apt.DESCRIPTION ACCOUNT_PLAN_TYPE_DESCRIPTION");
            sql.Append(" FROM TRANSACTIONS tr");
            sql.Append(" INNER JOIN ACCOUNT_PLANS ap on tr.ACCOUNT_PLAN_ID = ap.ID");
             sql.Append(" INNER JOIN ACCOUNT_PLAN_TYPES apt on ap.ACCOUNT_PLAN_TYPE_ID = apt.ID");

            if (startDate != null)
            {
                sql.Append($" WHERE tr.DATE >= '{startDate?.ToString("yyyy-MM-dd")}'");

                if (endDate != null)
                {
                    sql.Append($" AND tr.DATE <= '{endDate?.ToString("yyyy-MM-dd")}'");
                }
            
            }
            else
            {
                sql.Append($" WHERE tr.DATE <= '{endDate?.ToString("yyyy-MM-dd")}'");
            }

            sql.Append(" ORDER BY tr.DATE");

            DataTable dataTable = dalInstance.Select(sql.ToString());

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                TransactionModel transaction = new TransactionModel()
                {
                    Id = int.Parse(dataTable.Rows[i]["ID"].ToString()),
                    Date = DateTime.Parse(dataTable.Rows[i]["DATE"].ToString()),
                    Value = decimal.Parse(dataTable.Rows[i]["VALUE"].ToString()),
                    Description = dataTable.Rows[i]["DESCRIPTION"].ToString(),
                    AccountPlanDescription = dataTable.Rows[i]["ACCOUNT_PLAN_DESCRIPTION"].ToString(),
                    AccountPlanTypeDescription = dataTable.Rows[i]["ACCOUNT_PLAN_TYPE_DESCRIPTION"].ToString()
                };

                transactions.Add(transaction);
            }

            dalInstance.Disconnect();

            return transactions;
        }
    }
}