using ClassLibrary1;
using System;
using System.Threading;

namespace EmreBaserStairs
{
    class Program
    {
        static void Main(string[] args)
        {
            Stairs stairs = new Stairs();
            bool exit = false;

            Console.WriteLine("Press s to start or resume Stairs");
            Console.WriteLine("Press x to pause Stairs");
            Console.WriteLine("Press r to start the Stairs from the beginning");
            Console.WriteLine("Press q to quit the application");

            while (!exit)
            {
                Console.WriteLine();
                var keyInfo = Console.ReadKey(intercept: true);
                char choice = keyInfo.KeyChar;
                Console.WriteLine(choice);
                 
                switch (choice)
                {
                    case 's':
                        stairs.Start();
                        break;
                    case 'x':
                        stairs.Pause();
                        break;
                    case 'r':
                        stairs.Restart();
                        break;
                    case 'q':
                        stairs.Stop();
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        Thread.Sleep(2000); 
                        break;
                }
            }
        }
    }
}
