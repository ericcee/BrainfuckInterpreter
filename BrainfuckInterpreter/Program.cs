using System;
using System.IO;

namespace BrainfuckInterpreter
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("BF: File to load> ");
            string file = Console.ReadLine();
            byte[] code = File.ReadAllBytes(file);
            BFIntepreter bfi = new BFIntepreter(code, 1024 * 1024 );
            DateTime lprev = DateTime.Now;
            bfi.Run();
            TimeSpan ts = DateTime.Now - lprev; 

            Console.WriteLine($"\nExecution Time: {ts.TotalMilliseconds}ms");
            Console.ReadLine();
        }
    }
}
