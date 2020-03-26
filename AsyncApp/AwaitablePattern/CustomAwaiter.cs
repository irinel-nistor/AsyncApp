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
        private bool continueOnCapturedContext = true;
        private Exception exception;
        private Action savedContinuation;
        private SynchronizationContext savedSynchronizationContext = SynchronizationContext.Current;
        private static EventWaitHandle eventWaitHandle = new EventWaitHandle(false, EventResetMode.ManualReset);
        
        public void SetResult()
        {
            IsCompleted = true;
            InvokeSavedContinuation();
            eventWaitHandle.Set();
        }

        public void SetException(Exception exception)
        {
            IsCompleted = true;
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
            if (IsCompleted)
            {
                continuation();
            }
            else
            {
                savedContinuation = continuation;
            }
        }

        public bool IsCompleted { get; private set; } = false;

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
            if (this.continueOnCapturedContext && this.savedSynchronizationContext != null)
            {
                savedSynchronizationContext.Post(delegate { savedContinuation?.Invoke(); }, null);
            }
            else
            {
                savedContinuation?.Invoke();
            }
        }
    }
}
