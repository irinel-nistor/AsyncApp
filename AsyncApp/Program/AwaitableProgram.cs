using AsyncApp.AwaitablePattern;
using AsyncApp.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncApp.Program
{
    public class AwaitableProgram
    {
        public async Task Run()
        {
            Console.WriteLine("\nAwaitable and Awaiter");
            var fakeDatabase = new FakeDatabase();
            fakeDatabase.OnWriteCompleted += (o, e) =>
            {
                Console.WriteLine("Database record written");
            };

            await fakeDatabase.WriteToDatabaseAsync();
            Console.WriteLine("after Awaitable and Awaiter");
        }
    }
}
