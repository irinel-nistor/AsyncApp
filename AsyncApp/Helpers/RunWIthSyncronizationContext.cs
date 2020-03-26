using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncApp.Helpers
{
    class RunWIthSyncronizationContext
    {
        public static void Run(Func<Task> func)
        {
            var prevCtx = SynchronizationContext.Current;
            try
            {
                var syncCtx = new CustomSynchronizationContext();
                SynchronizationContext.SetSynchronizationContext(syncCtx);
                var t = func();
                t.ConfigureAwait(false);
                t.ContinueWith(
                    delegate { syncCtx.Complete(); }, TaskScheduler.Default);

                syncCtx.RunOnCurrentThread();
                t.GetAwaiter().GetResult();
            }
             finally 
            {
                SynchronizationContext.SetSynchronizationContext(prevCtx); 
            }
        }
    }
}
