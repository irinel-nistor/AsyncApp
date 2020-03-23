using AsyncApp.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AsyncApp.Program
{
    public class AwaitableExceptionProgram
    {
        public async Task Run()
        {
            Console.WriteLine("\nAwaitable with exception");
            var fakeDatabase = new FakeDatabase();
            fakeDatabase.OnWriteCompleted += (o, e) =>
            {
                Console.WriteLine("Database record written");
            };

            await fakeDatabase.WriteToDatebaseAsyncWithError();
            Console.WriteLine("after Awaitable with exception");
        }
    }
}
