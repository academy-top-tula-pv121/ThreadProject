namespace ThreadProject
{
    class Summa
    {
        int num;
        public int Sum { get; private set; }
        public Summa(int num)  => this.num = num;
        public void Run()
        {
            Sum = 0;
            for (int i = 1; i <= num; i++)
            {
                Sum += i;
                //Thread.Sleep(1);
            }
        }

        public void InitRun(object num)
        {
            if (num is not int) return;

            this.num = (int)num;
            Sum = 0;
            for (int i = 1; i <= this.num; i++)
            {
                Sum += i;
                //Thread.Sleep(1);
            }
            Console.WriteLine($"Summa from 1 to {this.num} = {Sum}");
        }
    }

    class ClassRun
    {
        bool done;
        public void Run()
        {
            if (!done)
            {
                done = true;
                Console.WriteLine("Done");
            }
        }
    }

    
    internal class Program
    {
        static bool isDone;
        static void LocalRun()
        {
            for (int i = 0; i < 10; i++)
                Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} - {i}");
        }

        static void GlobalRun()
        {
            if (!isDone) 
            {
                Console.WriteLine("Done");
                isDone = true;
            }
        }

        static void PrintName(object name)
        {
            Console.WriteLine($"Hello {name}");
        }
        static void Main(string[] args)
        {
            Thread thread = Thread.CurrentThread;
            Console.WriteLine($"Name: {thread.Name}");
            thread.Name = "My Main thred";
            Console.WriteLine($"New Name: {thread.Name}");

            Console.WriteLine($"Is Alive: {thread.IsAlive}");
            Console.WriteLine($"Id: {thread.ManagedThreadId}");
            Console.WriteLine($"Priority: {thread.Priority}");
            Console.WriteLine($"State: {thread.ThreadState}");

            Console.WriteLine($"Domain: {Thread.GetDomain().FriendlyName}");


            //Thread threadRun = new Thread(() => { 
            //    for(int i = 0; i < 30; i++)
            //    {
            //        Console.WriteLine(i);
            //        Thread.Sleep(300);
            //    }    
            //});
            //threadRun.IsBackground = true;
            ////threadRun.Start();
            ////threadRun.Join();

            //Summa summa = new(1000);
            //Thread summaThread = new Thread(summa.Run);
            //summaThread.Start();
            ////Thread.Sleep(20);
            //summaThread.Join();
            //Console.WriteLine(summa.Sum);


            // локальные свойства потоков
            //new Thread(LocalRun).Start();
            //LocalRun();

            //глобальные разделенные свойства потоков
            //new Thread(GlobalRun).Start();
            //GlobalRun();

            // замкнутые в лямбда разделенные свойства потоков
            //bool done = false;
            //ThreadStart action = () =>
            //{
            //    if (!done)
            //    {
            //        done = true;
            //        Console.WriteLine("Done");
            //    }
            //};

            //new Thread(action).Start();
            //action();

            // поля класса - разделенные свойства потоков
            //ClassRun obj = new();
            //new Thread(obj.Run).Start();
            //obj.Run();

            Thread th1 = new Thread(new ParameterizedThreadStart(PrintName));
            th1.Start("Joe");
            Thread th2 = new Thread(PrintName);
            th2.Start("Bob");
            Thread th3 = new Thread(name => Console.WriteLine($"By by {name}"));
            th3.Start("Tim");

            Summa summa = new Summa(10);
            Thread thSum = new Thread(summa.InitRun);
            thSum.Start("500");
        }
    }
}