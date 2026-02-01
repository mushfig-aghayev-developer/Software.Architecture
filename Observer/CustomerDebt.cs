namespace Observer
{
    // Customer debt information
    public class CustomerDebt
    {
        public string CustomerName { get; set; }
        public decimal TotalDebt { get; set; }
        public decimal MonthlyPayment { get; set; }
        public decimal RemainingDebt { get; set; }
        public int CurrentMonth { get; set; }
        public List<PaymentRecord> PaymentHistory { get; set; }

        public CustomerDebt(string customerName, decimal totalDebt, decimal monthlyPayment)
        {
            CustomerName = customerName;
            TotalDebt = totalDebt;
            MonthlyPayment = monthlyPayment;
            RemainingDebt = totalDebt;
            CurrentMonth = 0;
            PaymentHistory = new List<PaymentRecord>();
        }
    }
}