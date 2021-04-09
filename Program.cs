using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThread
{
    class Program
    {
        static void Main(string[] args)
        {
            SingleThread();
            DoubleThread();
            UsingTasks();
        }

        static void SingleThread()
        {
            //executou em 80s
            var start = DateTime.Now;
            
            var counter = CreateList(1000000000);

            var finish = DateTime.Now;
            var diferenca = finish - start;
            Console.WriteLine("Executei Single Thread");
            Console.WriteLine($"Salvei {counter} entradas. Tempo: {diferenca}");
        }

        static void DoubleThread()
        {
            //executa em 60s
            var list = new List<int>();
            
            Thread thread1 = new Thread(() => CreateList(500000000));
            Thread thread2 = new Thread(() => CreateList(500000000));

            var start = DateTime.Now;
            
            thread1.Start();
            thread2.Start();

            while (thread1.IsAlive || thread2.IsAlive)
            {
                Thread.Sleep(500);
            }

            var finish = DateTime.Now;

            var diferenca = finish - start;
            Console.WriteLine("Executei Dual Thread");
            Console.WriteLine($"Salvei as entradas usando 2 threads. Tempo: {diferenca}");
        }

        static void UsingTasks()
        {
        //executou em 49s
        var start = DateTime.Now;

        var task = Task.Factory.StartNew(() => CreateList(1000000000));

        task.Wait();
        
        var finish = DateTime.Now;

        var diferenca = finish - start;
        Console.WriteLine("Executei Task");
        Console.WriteLine($"Salvei as entradas usando tasks. Tempo: {diferenca}");
        }

        static int CreateList(int maxCounter)
        {
            var list = new List<int>();
            var counter = 0;    
            while (counter <= maxCounter)
            {
                list.Add(counter);
                counter++;
            }     
            return counter;   
        }

    }
}
