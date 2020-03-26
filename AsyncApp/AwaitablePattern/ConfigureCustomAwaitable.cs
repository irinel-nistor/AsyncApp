using System;
using System.Collections.Generic;
using System.Text;

namespace AsyncApp.AwaitablePattern
{
    public class ConfigureCustomAwaitable : CustomAwaitable
    {
        public ConfigureCustomAwaitable(CustomAwaiter customAwaiter) : base(customAwaiter)
        {
        }
    }
}
