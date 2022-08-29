namespace myfinance_web_netcore.Models
{
    public class TransactionsReportModel
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public List<TransactionModel> Transactions { get; set; }

        public TransactionsReportModel()
        {
            Transactions = new List<TransactionModel>();
        }        
    }
}