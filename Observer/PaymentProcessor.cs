namespace Observer
{
    // Concrete Subject - Payment Processor
    public class PaymentProcessor : IPaymentSubject<PaymentEvent>
    {
        private List<IPaymentObserver<PaymentEvent>> _observers = new List<IPaymentObserver<PaymentEvent>>();
        private Dictionary<string, CustomerDebt> _customerDebts = new Dictionary<string, CustomerDebt>();
        private int _nextPaymentId = 1001;

        public void Attach(IPaymentObserver<PaymentEvent> observer)
        {
            _observers.Add(observer);
            Console.WriteLine($"Observer {observer.GetType().Name} attached to Payment Processor");
        }

        public void Detach(IPaymentObserver<PaymentEvent> observer)
        {
            _observers.Remove(observer);
            Console.WriteLine($"Observer {observer.GetType().Name} detached from Payment Processor");
        }

        public void Notify(PaymentEvent eventData)
        {
            Console.WriteLine($"\n--- Notifying {_observers.Count} observers about {eventData.EventType} ---");
            foreach (var observer in _observers)
            {
                observer.Execute(eventData);
            }
        }

        /*=====================================================================================*/
        public void AddCustomerDebt(string customerName, decimal totalDebt, decimal monthlyPayment)
        {
            _customerDebts[customerName] = new CustomerDebt(customerName, totalDebt, monthlyPayment);
            Console.WriteLine($"💰 Added debt record: {customerName} - Total: ${totalDebt:F2}, Monthly: ${monthlyPayment:F2}");
        }

        public void ProcessMonthlyPayment(string customerName, int monthNumber, string monthName)
        {
            if (!_customerDebts.ContainsKey(customerName))
            {
                Console.WriteLine($"❌ Customer {customerName} not found!");
                return;
            }

            var customerDebt = _customerDebts[customerName];
            customerDebt.CurrentMonth = monthNumber;

            // Calculate payment (last month might be different)
            decimal paymentAmount = Math.Min(customerDebt.MonthlyPayment, customerDebt.RemainingDebt);
            customerDebt.RemainingDebt -= paymentAmount;

            // Add to payment history
            var paymentRecord = new PaymentRecord
            {
                Month = monthNumber,
                MonthName = monthName,
                PaymentAmount = paymentAmount,
                RemainingDebt = customerDebt.RemainingDebt,
                PaymentDate = DateTime.Now,
                Status = PaymentStatus.Completed.ToString()
            };
            customerDebt.PaymentHistory.Add(paymentRecord);

            // Create payment event
            var paymentEvent = new PaymentEvent(
                _nextPaymentId++,
                paymentAmount,
                customerName,
                PaymentEventStatus.Monthly_Payment,
                PaymentStatus.Completed,
                monthNumber,
                customerDebt.RemainingDebt
            );

            Console.WriteLine($"\n🔄 Processing {monthName} payment for {customerName}");
            Console.WriteLine($"   Payment Amount: ${paymentAmount:F2}");
            Console.WriteLine($"   Remaining Debt: ${customerDebt.RemainingDebt:F2}");

            // Notify observers
            Notify(paymentEvent);
        }

        public void PrintInvoice(string customerName, int monthNumber)
        {
            if (!_customerDebts.ContainsKey(customerName))
                return;

            var customerDebt = _customerDebts[customerName];
            var payment = customerDebt.PaymentHistory.FirstOrDefault(p => p.Month == monthNumber);

            if (payment != null)
            {
                var paymentEvent = new PaymentEvent(
                    _nextPaymentId,
                    payment.PaymentAmount,
                    customerName,
                    PaymentEventStatus.Invoice_Printed,
                    PaymentStatus.InvoiceSent,
                    monthNumber,
                    payment.RemainingDebt
                );

                Console.WriteLine($"\n🖨️  Printing invoice for {customerName} - Month {monthNumber}");
                Notify(paymentEvent);
            }
        }

        public CustomerDebt GetCustomerDebt(string customerName)
        {
            return _customerDebts.ContainsKey(customerName) ? _customerDebts[customerName] : null;
        }
    }
}