using System;

namespace MemoryUsageExample
{
    class Program
    {
        static void Main()
        {
            long memoryUsage = GC.GetTotalMemory(false);
            Console.WriteLine($"Current memory usage: {memoryUsage} bytes");
        }
    }
}
