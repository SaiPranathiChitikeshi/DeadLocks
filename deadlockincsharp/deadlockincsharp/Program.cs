using System;
using System.Threading;
namespace deadlockincsharp
{
    public class Program
    {
        static readonly object firstLock = new object();
        static readonly object secondLock = new object();
        static void ThreadJob()
        {
            Console.WriteLine("\t\tLocking firstLock");
            lock (firstLock)
            {
                Console.WriteLine("\t\tLocked firstLock");
                
                Thread.Sleep(1000);
                Console.WriteLine("\t\tLocking secondLock");
                lock (secondLock)
                {
                    Console.WriteLine("\t\tLocked secondLock");
                }
                Console.WriteLine("\t\tReleased secondLock");
            }
            Console.WriteLine("\t\tReleased firstLock");
        }
        static void Main()
        {
            new Thread(new ThreadStart(ThreadJob)).Start();
            
            Thread.Sleep(500);
            Console.WriteLine("Locking secondLock");
            lock (secondLock)
            {
                Console.WriteLine("Locked secondLock");
                Console.WriteLine("Locking firstLock");
                lock (firstLock)
                {
                    Console.WriteLine("Locked firstLock");
                }
                Console.WriteLine("Released firstLock");
            }
            Console.WriteLine("Released secondLock");
            Console.Read();
        }
    }
}