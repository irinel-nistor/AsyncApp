using AsyncApp.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncApp.Program
{
    public class ConfigureAwaitProgram
    {
        public void Run()
        {
            //var fileWriter = new FileWriter();
            //RunWIthSyncronizationContext.Run(async () =>
            //{
            //    await fileWriter.WriteToFileAsync("text.txt", "S");
            //    Console.WriteLine("Run on main thread");
            //});

            //RunWIthSyncronizationContext.Run(async () =>
            //{
            //    fileWriter.WriteToFileAsync("text.txt", "S").GetAwaiter().GetResult();
            //    Console.WriteLine("Run on main thread");
            //});

            var fakeDatabase = new FakeDatabase();
            RunWIthSyncronizationContext.Run(async () =>
            {
                fakeDatabase.WriteToDatabaseAsyncCurrentContext().GetAwaiter().GetResult();
            });

            RunWIthSyncronizationContext.Run(async () =>
            {
                await fakeDatabase.WriteToDatabaseAsyncCurrentContext();
            });
        }
    }
}
