using AsyncApp.Helpers;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncApp.AwaitablePattern
{
    public class CustomAwaiter : INotifyCompletion, ICriticalNotifyCompletion
    {
        private bool isCompleted = false;
        private bool continueOnCapturedContext = true;
        private Exception exception;
        private Action savedContinuation;
        private static EventWaitHandle eventWaitHandle = new EventWaitHandle(false, EventResetMode.ManualReset);

        public void SetResult()
        {
            isCompleted = true;
            InvokeSavedContinuation();
            eventWaitHandle.Set();
        }

        public void SetException(Exception exception)
        {
            isCompleted = true;
            this.exception = exception;
            eventWaitHandle.Set();
            InvokeSavedContinuation();
        }

        public void OnCompleted(Action continuation)
        {
            continuation();
        }

        public void UnsafeOnCompleted(Action continuation)
        {
            if (isCompleted)
            {
                continuation();
            }
            else
            {
                savedContinuation = continuation;
            }
        }

        public bool IsCompleted { get { return isCompleted; } }

        public void GetResult()
        {
            Console.WriteLine("GetResult was called");
            if (!IsCompleted)
            {
                eventWaitHandle.WaitOne(Timeout.Infinite);
            }

            if (IsCompleted && exception != null)
            {
                throw exception;
            }
        }

        public ConfigureCustomAwaitable ConfigureAwait(bool continueOnCapturedContext)
        {
            this.continueOnCapturedContext = continueOnCapturedContext;
            return new ConfigureCustomAwaitable(this);
        }

        private void InvokeSavedContinuation()
        {
            var synchronizationContext = SynchronizationContext.Current;
            if (this.continueOnCapturedContext && synchronizationContext != null)
            {
                synchronizationContext.Post(delegate { savedContinuation?.Invoke(); }, null);
            }
            else
            {
                savedContinuation?.Invoke();
            }
        }
    }
}
