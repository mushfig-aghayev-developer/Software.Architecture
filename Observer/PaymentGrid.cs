namespace Observer
{
    // Concrete Observer - Payment Grid
    public class PaymentGrid : IPaymentObserver<PaymentEvent>
    {
        private List<PaymentEvent> _payments { get; set; }

        public PaymentGrid()
        {
            //from DB or other source, for demo we initialize an empty list
            _payments = new List<PaymentEvent>();
        }

        public void Execute(PaymentEvent eventData)
        {
            if (eventData.EventType == PaymentEventStatus.Monthly_Payment)
            {
                _payments.Add(eventData);
                Console.WriteLine($"📊 PaymentGrid: Added Month {eventData.MonthNumber} payment for {eventData.CustomerName}");
            }
            else if (eventData.EventType == PaymentEventStatus.Invoice_Printed)
            {
                var payment = _payments.Find(p => p.PaymentId == eventData.PaymentId && p.MonthNumber == eventData.MonthNumber);
                if (payment != null)
                {
                    payment.Status = PaymentStatus.InvoiceSent;
                    Console.WriteLine($"📊 PaymentGrid: Updated Month {eventData.MonthNumber} status to 'Invoice Sent'");
                }
            }
        }

        public void DisplayCurrentGrid()
        {
            Console.WriteLine("\n📊 === CURRENT PAYMENT GRID ===");
            Console.WriteLine("ID\t| Customer\t| Month\t| Amount\t| Remaining\t| Status");
            Console.WriteLine(new string('-', 75));

            foreach (var payment in _payments.OrderBy(p => p.MonthNumber))
            {
                Console.WriteLine($"{payment.PaymentId}\t| {payment.CustomerName,-10}\t| {payment.MonthNumber,-5}\t| ${payment.Amount,-8:F2}\t| ${payment.RemainingDebt,-10:F2}\t| {payment.Status}");
            }
            Console.WriteLine(new string('-', 75));
        }
    }
}