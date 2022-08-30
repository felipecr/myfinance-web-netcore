using System.Data;
using myfinance_web_netcore.Infra;
using myfinance_web_netcore.Models;

namespace myfinance_web_netcore.Domain
{
    public class AccountPlanType
    {
        public List<AccountPlanTypeModel> getAccountPlanTypes()
        {
            List<AccountPlanTypeModel> accountPlanTypes = new List<AccountPlanTypeModel>();
            DAL dalInstance = DAL.GetInstance;
            dalInstance.Connect();

            string sql = "SELECT ID, CODE, DESCRIPTION FROM ACCOUNT_PLAN_TYPES";
            DataTable dataTable = dalInstance.Select(sql);

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                AccountPlanTypeModel accountPlanType = new AccountPlanTypeModel()
                {
                    Id = int.Parse(dataTable.Rows[i]["ID"].ToString()),
                    Code = char.Parse(dataTable.Rows[i]["CODE"].ToString()),
                    Description = dataTable.Rows[i]["DESCRIPTION"].ToString()
                };

                accountPlanTypes.Add(accountPlanType);
            }

            dalInstance.Disconnect();

            return accountPlanTypes;
        }
    }
}