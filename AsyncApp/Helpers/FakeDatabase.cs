using AsyncApp.AwaitablePattern;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncApp.Helpers
{
    public class FakeDatabase
    {
        public event EventHandler OnWriteCompleted;
        public event EventHandler<Exception> OnExceptionThrown;

        public CustomAwaitable WriteToDatabaseAsync()
        {
            // Create and start the asyncronous operation
            Task.Factory.StartNew(async () => {
                await Task.Delay(500);
                Console.WriteLine("Ongoing async operation");
                await Task.Delay(500);
                OnWriteCompleted(this, new EventArgs());
            });

            // Set event to call continuation and set isComplete to true 
            var awaiter = new CustomAwaiter();
            OnWriteCompleted += (o, e) => awaiter.SetResult();
            
            // Create and return a Task like object
            return new CustomAwaitable(awaiter);
        }

        public CustomAwaitable WriteToDatebaseAsyncWithError()
        {
            // Create and start the asyncronous operation
            Task.Factory.StartNew(async () => {
                try
                {                     
                    await Task.Delay(400);
                    Console.WriteLine("Ongoing async operation");
                    throw new Exception("Async operation failed");
                    Console.WriteLine("After exception - ongoing async operation");
                    await Task.Delay(400);
                    OnWriteCompleted(this, new EventArgs());
                }
                catch (Exception ex)
                {
                    OnExceptionThrown(this, ex);
                }
            });

            // Set event to call continuation and set isComplete to true 
            var awaiter = new CustomAwaiter();
            OnWriteCompleted += (o, e) => awaiter.SetResult();

            // set event to call set exception
            OnExceptionThrown += (o, ex) => awaiter.SetException(ex);

            // Create and return a Task like object
            return new CustomAwaitable(awaiter);
        }

        public CustomAwaitable WriteToDatabaseAsyncCurrentContext()
        {
            // Create and start the asyncronous operation
            Task.Factory.StartNew(async () => {
                await Task.Delay(500);
                Console.WriteLine("Ongoing async operation");
                await Task.Delay(500);
                OnWriteCompleted(this, new EventArgs());
            }, CancellationToken.None
            , TaskCreationOptions.None
            , TaskScheduler.FromCurrentSynchronizationContext());

            // Set event to call continuation and set isComplete to true 
            var awaiter = new CustomAwaiter();
            OnWriteCompleted += (o, e) => awaiter.SetResult();

            // Create and return a Task like object
            return new CustomAwaitable(awaiter);
        }
    }
}
