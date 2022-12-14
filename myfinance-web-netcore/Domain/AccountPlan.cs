using System.Data;
using System.Text;
using myfinance_web_netcore.Infra;
using myfinance_web_netcore.Models;

namespace myfinance_web_netcore.Domain
{
    public class AccountPlan
    {
        public void Insert(AccountPlanModel form)
        {
            DAL dalInstance = DAL.GetInstance;
            dalInstance.Connect();

            string sql = $"INSERT INTO ACCOUNT_PLANS(DESCRIPTION, ACCOUNT_PLAN_TYPE_ID) VALUES ('{form.Description}', '{form.AccountPlanTypeId}')";

            dalInstance.ExecuteCommand(sql);
            dalInstance.Disconnect();
        }

        public void Update(AccountPlanModel form)
        {
            DAL dalInstance = DAL.GetInstance;
            dalInstance.Connect();

            string sql = $"UPDATE ACCOUNT_PLANS SET DESCRIPTION = '{form.Description}', ACCOUNT_PLAN_TYPE_ID = '{form.AccountPlanTypeId}' WHERE ID = {form.Id}";

            dalInstance.ExecuteCommand(sql);
            dalInstance.Disconnect();
        }

        public void Delete(int id)
        {
            DAL dal = DAL.GetInstance;
            dal.Connect();
            string sql = $"DELETE FROM ACCOUNT_PLANS WHERE ID = {id}";
            dal.ExecuteCommand(sql);
            dal.Disconnect();
        }

        public AccountPlanModel GetAccountPlanById(int? id)
        {
            DAL dalInstance = DAL.GetInstance;
            dalInstance.Connect();

            string sql = $"SELECT ID, DESCRIPTION, ACCOUNT_PLAN_TYPE_ID FROM ACCOUNT_PLANS WHERE ID = {id}";
            DataTable dataTable = dalInstance.Select(sql);

            AccountPlanModel accountPlan = new AccountPlanModel()
            {
                Id = int.Parse(dataTable.Rows[0]["ID"].ToString()),
                Description = dataTable.Rows[0]["DESCRIPTION"].ToString(),
                AccountPlanTypeId = int.Parse(dataTable.Rows[0]["ACCOUNT_PLAN_TYPE_ID"].ToString())
            };

            dalInstance.Disconnect();

            return accountPlan;
        }

        public List<AccountPlanModel> getAccountPlans()
        {
            List<AccountPlanModel> accountPlans = new List<AccountPlanModel>();
            DAL dalInstance = DAL.GetInstance;
            dalInstance.Connect();

            StringBuilder sql = new StringBuilder("SELECT ap.ID, ap.DESCRIPTION, ap.ACCOUNT_PLAN_TYPE_ID, apt.DESCRIPTION TYPE_DESCRIPTION");
            sql.Append(" FROM ACCOUNT_PLANS ap");
            sql.Append(" INNER JOIN ACCOUNT_PLAN_TYPES apt on ap.ACCOUNT_PLAN_TYPE_ID = apt.ID");

            DataTable dataTable = dalInstance.Select(sql.ToString());

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                AccountPlanModel accountPlan = new AccountPlanModel()
                {
                    Id = int.Parse(dataTable.Rows[i]["ID"].ToString()),
                    Description = dataTable.Rows[i]["DESCRIPTION"].ToString(),
                    AccountPlanTypeId = int.Parse(dataTable.Rows[i]["ACCOUNT_PLAN_TYPE_ID"].ToString()),
                    AccountPlanTypeDescription = dataTable.Rows[i]["TYPE_DESCRIPTION"].ToString()
                };

                accountPlans.Add(accountPlan);
            }

            dalInstance.Disconnect();

            return accountPlans;
        }
    }
}