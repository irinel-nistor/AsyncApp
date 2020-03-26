using AsyncApp.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace AsyncApp.AwaitablePattern
{
    public class CustomAwaitable
    {
        private CustomAwaiter awaiter;

        public CustomAwaitable(CustomAwaiter awaiter)
        {
            this.awaiter = awaiter;
        }

        public CustomAwaiter GetAwaiter()
        {
            return awaiter;
        }

        public ConfigureCustomAwaitable ConfigureAwait(bool continueOnCapturedContext)
        {
            return awaiter.ConfigureAwait(continueOnCapturedContext);
        }
    }
}
