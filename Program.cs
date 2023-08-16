using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AliSalmeh_ProjectWeek11_Breakfast_Refactor
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var watch = new Stopwatch();
            watch.Start();

            var cup = ReadyCoffee();
            Console.WriteLine("Coffee is ready");

            var eggTask = FryEggsAsync(2);
            var baconTask = FryBaconsAsync(3);
            var toastTask = ToastAndButterAsync(4);

            var breakfastTaskList = new List<Task> { eggTask, baconTask, toastTask };

            while (breakfastTaskList.Count > 0)
            {
                var finishTask = await Task.WhenAny(breakfastTaskList);
                if (finishTask == eggTask)
                {
                    Console.WriteLine("Eggs are ready");
                }
                else if (finishTask == baconTask)
                {
                    Console.WriteLine("Bacon is ready");

                }
                else if (finishTask == toastTask)
                {
                    Console.WriteLine("Toast is ready");
                }
                breakfastTaskList.Remove(finishTask);
            }

            var juice = ReadyOrangeJuice();
            Console.WriteLine("Juice is ready");
            Console.WriteLine("Breakfast is ready");

            watch.Stop();
            Console.WriteLine(watch.ElapsedMilliseconds + "ms");
        }
        private static Coffee ReadyCoffee()
        {
            Console.WriteLine("Pouring coffee");
            return new Coffee();
        }
        private static async Task<Egg> FryEggsAsync(int howMany)
        {
            Console.WriteLine("Warming the pan");
            await Task.Delay(3000);
            return new Egg();
        }
        private static async Task<Bacon> FryBaconsAsync(int slices)
        {
            Console.WriteLine("Cooking first side of bacon");
            await Task.Delay(3000);
            for (int slice = 1; slice < slices; slice++)
            {
                Console.WriteLine("flipping a slice number " + slice);
            }
            Console.WriteLine("Cooking second side of bacon");
            await Task.Delay(3000);
            return new Bacon();
        }
        private static async Task<Toast> ToastBreaadAsync(int slices)
        {
            for (int slice = 1; slice < slices; slice++)
            {
                Console.WriteLine("Toasting slice number " + slice);
            }
            Console.WriteLine("Start toasting...");
            await Task.Delay(3000);
            Console.WriteLine("Remove toast from toaster");
            return new Toast();
        }
        private static void ApplyButter() => Console.WriteLine("Put butter on bread");
        private static void ApplyJam() => Console.WriteLine("Put jam on bread");
        private static async Task<Toast> ToastAndButterAsync(int number)
        {
            var toastTask = await ToastBreaadAsync(number);
            ApplyButter();
            ApplyJam();
            return toastTask;
        }
        private static Juice ReadyOrangeJuice()
        {
            Console.WriteLine("Pouring orange juice");
            return new Juice();
        }
    }
    class Coffee { }
    class Egg { }
    class Bacon { }
    class Toast { }
    class Juice { }
}
