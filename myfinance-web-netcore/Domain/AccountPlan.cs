using System.Data;
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

            string sql = $"INSERT INTO ACCOUNT_PLANS(DESCRIPTION, TYPE) VALUES ('{form.Description}', '{form.Type}')";

            dalInstance.ExecuteCommand(sql);
            dalInstance.Disconnect();
        }

        public void Update(AccountPlanModel form)
        {
            DAL dalInstance = DAL.GetInstance;
            dalInstance.Connect();

            string sql = $"UPDATE ACCOUNT_PLANS SET DESCRIPTION = '{form.Description}', TYPE = '{form.Type}' WHERE ID = {form.Id}";

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

            string sql = $"SELECT ID, DESCRIPTION, TYPE FROM ACCOUNT_PLANS WHERE ID = {id}";
            DataTable dataTable = dalInstance.Select(sql);

            AccountPlanModel accountPlan = new AccountPlanModel()
            {
                Id = int.Parse(dataTable.Rows[0]["ID"].ToString()),
                Description = dataTable.Rows[0]["DESCRIPTION"].ToString(),
                Type = dataTable.Rows[0]["TYPE"].ToString()
            };

            dalInstance.Disconnect();

            return accountPlan;
        }

        public List<AccountPlanModel> getAccountPlans()
        {
            List<AccountPlanModel> accountPlans = new List<AccountPlanModel>();
            DAL dalInstance = DAL.GetInstance;
            dalInstance.Connect();

            string sql = "SELECT ID, DESCRIPTION, TYPE FROM ACCOUNT_PLANS";
            DataTable dataTable = dalInstance.Select(sql);

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                AccountPlanModel accountPlan = new AccountPlanModel()
                {
                    Id = int.Parse(dataTable.Rows[i]["ID"].ToString()),
                    Description = dataTable.Rows[i]["DESCRIPTION"].ToString(),
                    Type = dataTable.Rows[i]["TYPE"].ToString()
                };

                accountPlans.Add(accountPlan);
            }

            dalInstance.Disconnect();

            return accountPlans;
        }
    }
}