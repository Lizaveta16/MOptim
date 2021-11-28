using System;

namespace Huka_Djivisa
{
    class Program
    {
        static void Main(string[] args)
        {
            var func = new Huka_Djivisa(5,
                new[] {
                    11.0 /2, 9.0/2, 3.0/2, 7.0/2, 6.0/2
                },
                 new[] {
                    1350 * 70.0,
                    1210 * 65,
                    1150 * 80,
                    1300 * 77,
                    890 * 93

                } );

            func.MinimazeFunction();
            
            Console.WriteLine("Done!");
            Console.ReadKey(true);
        }
    }
}
