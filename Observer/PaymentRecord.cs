namespace Observer
{
    // Payment record for tracking
    public class PaymentRecord
    {
        public int Month { get; set; }
        public string MonthName { get; set; }
        public decimal PaymentAmount { get; set; }
        public decimal RemainingDebt { get; set; }
        public DateTime PaymentDate { get; set; }
        public string Status { get; set; }
    }
}