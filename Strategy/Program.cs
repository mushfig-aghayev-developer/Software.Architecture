namespace Strategy
{
    public class Program
    {
        static void CheckNumbers(IDigitService digitService)
        {
            uint firstNumber = 16711;
            uint secondNumber = 76111;
            var result = digitService.Validate(firstNumber, secondNumber);
            Console.WriteLine(result);
        }
        public static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;

            CheckNumbers(new DigitService());
            CheckNumbers(new DigitalService_v2());

            Console.WriteLine("test");
            Console.ReadLine();
        }
    }
}