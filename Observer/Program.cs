using System;
using System.Collections.Generic;
using System.Linq;
using static Observer.PaymentEvent;

namespace Observer
{
    // Generic Observer interface
    public interface IPaymentObserver<T>
    {
        void Execute(T eventData);
    }

    // Subject interface
    public interface IPaymentSubject<T>
    {
        void Attach(IPaymentObserver<T> observer);
        void Detach(IPaymentObserver<T> observer);
        void Notify(T eventData);
    }

    public enum PaymentStatus
    {
        Pending = 1,
        Completed = 2,
        InvoiceSent = 3,
        Failed = 4
    }

    public static class PaymentEventStatus
    {
        public const string Monthly_Payment = "Monthly Payment Processed";
        public const string Invoice_Printed = "Invoice Printed";
    }

    // Main Program
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("Generic Observer Pattern Demo - Debt Payment System");
            Console.WriteLine(new string('=', 70));

            // Create the subject (payment processor)
            var paymentProcessor = new PaymentProcessor();

            // Create observers
            var invoicePrinter = new InvoicePrinter();
            var paymentGrid = new PaymentGrid();
            var debtTracker = new DebtTracker();

            Console.WriteLine("\n Attaching observers to Payment Processor...");
            // Attach observers
            paymentProcessor.Attach(invoicePrinter);
            paymentProcessor.Attach(paymentGrid);
            paymentProcessor.Attach(debtTracker);

            // Add Michael Jackson with $12,000 debt
            Console.WriteLine("\n Setting up customer debt...");
            paymentProcessor.AddCustomerDebt("Michael Jackson", 12000m, 1000m);

            var michaelDebt = paymentProcessor.GetCustomerDebt("Michael Jackson");

            // Display BEFORE - full 12-month schedule
            DebtScheduleDisplay.DisplayFullSchedule(michaelDebt, "BEFORE - 12-Month Debt Payment Schedule");

            Console.WriteLine("\n" + new string('=', 70));
            Console.WriteLine("Processing payments for months 1-6...");

            // Process payments for months 1-6 (up to June)
            string[] monthNames = { "January", "February", "March", "April", "May", "June" };

            for (int month = 1; month <= 6; month++)
            {
                paymentProcessor.ProcessMonthlyPayment("Michael Jackson", month, monthNames[month - 1]);

                // Print invoice for this month
                paymentProcessor.PrintInvoice("Michael Jackson", month);

                Console.WriteLine(); // Space between months
            }

            // Display current payment grid
            paymentGrid.DisplayCurrentGrid();

            Console.WriteLine("\n Generic Observer Pattern Demo Completed!");
            Console.WriteLine($"Michael Jackson has paid ${michaelDebt.PaymentHistory.Sum(p => p.PaymentAmount):F2} so far");
            Console.WriteLine($"Remaining debt: ${michaelDebt.RemainingDebt:F2}");
            Console.WriteLine("\n Press any key to exit...");
            Console.ReadKey();
        }
    }
}