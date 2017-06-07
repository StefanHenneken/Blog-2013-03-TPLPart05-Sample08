using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sample08
{
    class Program
    {
        static void Main(string[] args)
        {
            new Program().Run();
        }
        public void Run()
        {
            ParallelLoopResult loopResult = new ParallelLoopResult();
            try
            {
                Console.WriteLine("\nParallel.For Start");
                loopResult = Parallel.For(0, 50000, (index) =>
                    {
                        if (index == 5000)
                            throw new IndexOutOfRangeException();

                        if (index == 49999)
                            throw new DivideByZeroException();

                        double temp = 1.1;

                        // doing some work
                        for (int i = 0; i < 1000; i++)
                        {
                            temp = Math.Sin(index) + Math.Sqrt(index) *
                                    Math.Pow(index, 3.1415) + temp /
                                    Math.Cosh(i + index * 0.73);
                        }
                    });
                Console.WriteLine("\nParallel.For End");
            }
            catch (AggregateException aex)
            {
                Console.WriteLine("\nAggregateException in Run: " + aex.Message);
                aex.Flatten();
                foreach (Exception ex in aex.InnerExceptions)
                    Console.WriteLine("  Exception: " + ex.Message);
            }
            finally
            {
                Console.WriteLine("\nloopResult.IsCompleted: " + loopResult.IsCompleted);
                Console.WriteLine("\nEnd Run");
                Console.ReadLine();
            }
        }
    }
}
