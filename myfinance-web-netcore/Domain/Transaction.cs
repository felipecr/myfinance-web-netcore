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

            string sql = "INSERT INTO TRANSACTIONS(DATE, VALUE, TYPE, HISTORY, ACCOUNT_PLAN_ID) " + 
                $"VALUES ('{form.Date.ToString("yyyy-MM-dd")}'," +
                $"{form.Value}, '{form.Type}', '{form.History}', {form.AccountPlanId})";

            dalInstance.ExecuteCommand(sql);
            dalInstance.Disconnect();
        }

        public void Update(TransactionModel form)
        {
            DAL dalInstance = DAL.GetInstance;
            dalInstance.Connect();

            string sql = $"UPDATE TRANSACTIONS SET DATE = '{form.Date.ToString("yyyy-MM-dd")}'," +
                $"VALUE = {form.Value}, TYPE = '{form.Type}', HISTORY = '{form.History}', " +
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

            string sql = $"SELECT ID, DATE, VALUE, TYPE, HISTORY, ACCOUNT_PLAN_ID FROM TRANSACTIONS WHERE ID = '{id}'";
            DataTable dataTable = dalInstance.Select(sql);

            TransactionModel transaction = new TransactionModel()
            {
                Id = int.Parse(dataTable.Rows[0]["ID"].ToString()),
                Date = DateTime.Parse(dataTable.Rows[0]["DATE"].ToString()),
                Value = decimal.Parse(dataTable.Rows[0]["VALUE"].ToString()),
                Type = dataTable.Rows[0]["TYPE"].ToString(),
                History = dataTable.Rows[0]["HISTORY"].ToString(),
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

            string sql = "SELECT ID, DATE, VALUE, TYPE, HISTORY, ACCOUNT_PLAN_ID FROM TRANSACTIONS";
            DataTable dataTable = dalInstance.Select(sql);

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                TransactionModel transaction = new TransactionModel()
                {
                    Id = int.Parse(dataTable.Rows[i]["ID"].ToString()),
                    Date = DateTime.Parse(dataTable.Rows[i]["DATE"].ToString()),
                    Value = decimal.Parse(dataTable.Rows[i]["VALUE"].ToString()),
                    Type = dataTable.Rows[i]["TYPE"].ToString(),
                    History = dataTable.Rows[i]["HISTORY"].ToString(),
                    AccountPlanId = int.Parse(dataTable.Rows[i]["ACCOUNT_PLAN_ID"].ToString())
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

            StringBuilder sql = new StringBuilder("SELECT tr.ID, tr.DATE, tr.VALUE, tr.TYPE, tr.HISTORY, ap.DESCRIPTION");
            sql.Append(" FROM TRANSACTIONS tr");
            sql.Append(" INNER JOIN ACCOUNT_PLANS ap on tr.ACCOUNT_PLAN_ID = ap.ID");

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
                    Type = dataTable.Rows[i]["TYPE"].ToString(),
                    History = dataTable.Rows[i]["HISTORY"].ToString(),
                    AccountPlanDescription = dataTable.Rows[i]["DESCRIPTION"].ToString()
                };

                transactions.Add(transaction);
            }

            dalInstance.Disconnect();

            return transactions;
        }
    }
}