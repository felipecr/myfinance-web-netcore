namespace myfinance_web_netcore.Models
{
    public class AccountPlanModel
    {
        public int? Id { get; set; }

        public string? Description { get; set; }

        public int AccountPlanTypeId { get; set; }

        public string AccountPlanTypeDescription { get; set; }
    }
}