﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace AsyncApp.Helpers
{
    public sealed class CustomSynchronizationContext : SynchronizationContext
    {
        private readonly
         BlockingCollection<KeyValuePair<SendOrPostCallback, object>> m_queue = new BlockingCollection<KeyValuePair<SendOrPostCallback, object>>();
               
        public override void Post(SendOrPostCallback d, object state)
        {
            m_queue.Add(new KeyValuePair<SendOrPostCallback, object>(d, state));
        }
               
        public void RunOnCurrentThread()
        {
            KeyValuePair<SendOrPostCallback, object> workItem;
            while (m_queue.TryTake(out workItem, Timeout.Infinite))
                workItem.Key(workItem.Value);
        }
               
        public void Complete() { m_queue.CompleteAdding(); }
    }
}
