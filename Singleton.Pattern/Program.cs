using System;

namespace Singleton.Pattern
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("Singleton Pattern Example in .NET 9\n");
        }
    }

    public class SingletonV1
    {
        private readonly static SingletonV1 _instance = new SingletonV1(); //<= Not Atomar Operation
        private SingletonV1()
        {
            Console.WriteLine("SingletonV1 instance created.");
        }
        public static SingletonV1 Instance
        {
            get { return _instance; }
        }
    }
    public class SingletonV2
    {        private readonly static Lazy<SingletonV2> _instance = new Lazy<SingletonV2>(() => new SingletonV2());

        private SingletonV2()
        {
            
        }

        public static SingletonV2 Instance
        {
            get { return _instance.Value; }
        }
    }

    public class SingletonV3
    {
        private static volatile SingletonV3 _instance;
        private static readonly object _lock = new object();

        // Private constructor to prevent instantiation
        private SingletonV3()
        {
            Console.WriteLine("Singleton instance created.");
        }
        public static SingletonV3 Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new SingletonV3();
                        }
                    }
                }

                return _instance;
            }
        }
        
        public void DoSomething()
        {
            Console.WriteLine("Doing something with the singleton instance.");
        }
    }
}
