namespace Observer
{
    // Concrete Observer - Invoice Printer
    public class InvoicePrinter : IPaymentObserver<PaymentEvent>
    {
        public void Execute(PaymentEvent eventData)
        {
            if (eventData.EventType == PaymentEventStatus.Monthly_Payment)
            {
                Console.WriteLine($"📄 InvoicePrinter: Generating invoice for Payment #{eventData.PaymentId}");
                Console.WriteLine($"   Customer: {eventData.CustomerName}");
                Console.WriteLine($"   Month {eventData.MonthNumber} Payment: ${eventData.Amount:F2}");
                Console.WriteLine($"   Remaining Debt: ${eventData.RemainingDebt:F2}");
            }
            else if (eventData.EventType == PaymentEventStatus.Invoice_Printed)
            {
                Console.WriteLine($"📄 InvoicePrinter: Invoice #{eventData.PaymentId} sent to {eventData.CustomerName}");
            }
        }
    }
}