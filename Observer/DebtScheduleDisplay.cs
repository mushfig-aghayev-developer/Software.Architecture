namespace Observer
{
    // Helper class to display 12-month debt schedule
    public class DebtScheduleDisplay
    {
        public static void DisplayFullSchedule(CustomerDebt customerDebt, string title)
        {
            Console.WriteLine($"\n{title}");
            Console.WriteLine(new string('=', 80));
            Console.WriteLine($"Customer: {customerDebt.CustomerName}");
            Console.WriteLine($"Total Debt: ${customerDebt.TotalDebt:F2}");
            Console.WriteLine($"Monthly Payment: ${customerDebt.MonthlyPayment:F2}");
            Console.WriteLine();

            Console.WriteLine("Month | Month Name | Payment Amount | Remaining Debt | Status");
            Console.WriteLine(new string('-', 65));

            string[] monthNames = { "January", "February", "March", "April", "May", "June",
                                  "July", "August", "September", "October", "November", "December" };

            decimal remainingDebt = customerDebt.TotalDebt;

            for (int month = 1; month <= 12; month++)
            {
                decimal paymentAmount = Math.Min(customerDebt.MonthlyPayment, remainingDebt);
                remainingDebt -= paymentAmount;

                var existingPayment = customerDebt.PaymentHistory.FirstOrDefault(p => p.Month == month);
                string status = existingPayment?.Status ?? "Pending";

                if (remainingDebt < 0) remainingDebt = 0;

                Console.WriteLine($"{month,2}    | {monthNames[month - 1],-10} | ${paymentAmount,10:F2}   | ${remainingDebt,11:F2}   | {status}");

                if (remainingDebt <= 0 && month < 12)
                {
                    Console.WriteLine($"      | {"(Debt Paid Off)",-10} | {"--",10}   | {"--",11}   | --");
                    break;
                }
            }
            Console.WriteLine(new string('-', 65));
        }
    }
}