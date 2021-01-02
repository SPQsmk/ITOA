using System;
using System.IO;

namespace NewSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            string num, sys;

            switch (args.Length)
            {
                case 1:
                    try
                    {
                        using var sr = new StreamReader(args[0]);
                        num = sr.ReadLine();
                        sys = sr.ReadLine();
                    }
                    catch (FileNotFoundException)
                    {
                        Console.WriteLine("Unknown file");
                        return;
                    }

                    break;
                default:
                    Console.WriteLine("Enter number: ");
                    num = Console.ReadLine();
                    Console.WriteLine("Enter system: ");
                    sys = Console.ReadLine();
                    break;
            }

            try
            {
                Console.WriteLine(ITOA.ConvertFraction(num, sys));
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
