namespace Observer
{
    // Concrete Observer - Debt Tracker
    public class DebtTracker : IPaymentObserver<PaymentEvent>
    {
        public void Execute(PaymentEvent eventData)
        {
            if (eventData.EventType == PaymentEventStatus.Monthly_Payment)
            {
                Console.WriteLine($"💳 DebtTracker: Month {eventData.MonthNumber} - ${eventData.Amount:F2} paid, ${eventData.RemainingDebt:F2} remaining");

                if (eventData.RemainingDebt <= 0)
                {
                    Console.WriteLine($"🎉 DebtTracker: {eventData.CustomerName} has PAID OFF their debt!");
                }
            }
        }
    }
}