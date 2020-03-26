using AsyncApp.Helpers;
using System;

namespace AsyncApp.Program
{
    public class VoidAwaitableProgram
    {
        public void Run()
        {
            Console.WriteLine("\nVoid call");
            var fakeDatabase = new FakeDatabase();
            fakeDatabase.OnWriteCompleted += (o, e) =>
            {
                Console.WriteLine("void - Database record written");
            };

            var awaitable = fakeDatabase.WriteToDatebaseAsyncWithError();
            awaitable.GetAwaiter().GetResult();

            Console.WriteLine("After void call");
        }
    }
}
