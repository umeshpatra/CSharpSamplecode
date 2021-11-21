using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace asyncawaitsample
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World- Start");
            //CallRootMethod();
            await SpanThreads(10, 5000);
            Console.WriteLine("Hello World- End");
            Console.Read();
        }

        public static async Task SpanThreads(int threadcount, int delayinsecs)
        {
            List<Task> tasklist = new List<Task>();        
            IEnumerable<int> nums = Enumerable.Range(1,30).Select(x => x);
            
            var batchsize = 10;
            var batchcount = nums.Count() / batchsize; 
            // TODO : batchcount has to take care about the remainder as next page count
            var pageno = 0;

            while(pageno <= batchcount)
            {
                var currentrange = nums.Skip(pageno * batchsize).Take(batchsize);
                foreach(int num in currentrange)
                {
                    var newtask = Task.Run(() => {RunFile(num, delayinsecs);}); 
                    tasklist.Add(newtask);
                }
                await Task.WhenAll(tasklist);
                pageno++;
            }
        }

        public static void RunFile(int t, int delayinsecs)
        {         
            Console.WriteLine($"This is the sample thread {t} - start");
            Thread.Sleep(delayinsecs);
            Console.WriteLine($"This is the sample thread {t} - end");
        }
    }
}
