using AsyncApp.Helpers;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncApp.Program
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            // 0
            Console.WriteLine("Hello World!");
            var fileWriter = new FileWriter();
            await fileWriter.WriteToFileAsync("text.txt", "text");

            //1
            var iteratorProgram = new IteratorProgram();
            iteratorProgram.Run();

            //2
            var awaitableProgram = new AwaitableProgram();
            await awaitableProgram.Run();

            // 3
            var voidProgram = new VoidAwaitableProgram();
            //voidProgram.Run();

            //4
            var awaitableExceptionProgram = new AwaitableExceptionProgram();
            await awaitableExceptionProgram.Run();

            // 5
            var configureAwaitProgram = new ConfigureAwaitProgram();
            configureAwaitProgram.Run();

            //2
            var awaitableProgram2 = new AwaitableProgram();
            await awaitableProgram.Run();
        }
    }
}
