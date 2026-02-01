namespace Observer
{
    // Payment event data
    public class PaymentEvent
    {
        public int PaymentId { get; set; }
        public decimal Amount { get; set; }
        public string CustomerName { get; set; }
        public DateTime PaymentDate { get; set; }
        public string EventType { get; set; }
        public PaymentStatus Status { get; set; }
        public int MonthNumber { get; set; }
        public decimal RemainingDebt { get; set; }


        public PaymentEvent(int paymentId, decimal amount, string customerName, string eventType,
                          PaymentStatus status = PaymentStatus.Pending, int monthNumber = 0, decimal remainingDebt = 0)
        {
            PaymentId = paymentId;
            Amount = amount;
            CustomerName = customerName;
            PaymentDate = DateTime.Now;
            EventType = eventType;
            Status = status;
            MonthNumber = monthNumber;
            RemainingDebt = remainingDebt;
        }
    }
}