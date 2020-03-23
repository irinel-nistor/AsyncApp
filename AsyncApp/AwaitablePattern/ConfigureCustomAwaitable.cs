using System;
using System.Collections.Generic;
using System.Text;

namespace AsyncApp.AwaitablePattern
{
    public class ConfigureCustomAwaitable
    {
        private CustomAwaiter customAwaiter;

        public ConfigureCustomAwaitable(CustomAwaiter customAwaiter)
        {
            this.customAwaiter = customAwaiter;
        }

        public CustomAwaiter GetAwaiter()
        {
            return customAwaiter;
        }
    }
}
