namespace myfinance_web_netcore.Models
{
    public class TransactionModel
    {
        public int? Id { get; set; }

        public DateTime Date { get; set; }

        public decimal Value { get; set; }

        public string? Description { get; set; }

        public int AccountPlanId { get; set; }

        public string? AccountPlanDescription { get; set; }

        public string? AccountPlanTypeDescription { get; set; }
    }
}