using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace BEPUutilities.ResourceManagement
{
    /// <summary>
    /// Contains locking and thread static buffer pools for the specified type.
    /// </summary>
    /// <typeparam name="T">Type of element in the buffers stored in the pools.</typeparam>
    public static class BufferPools<T>
    {
        /// <summary>
        /// Gets a buffer pool for this type which provides thread safe resource acquisition and return.
        /// </summary>
        public static LockingBufferPool<T> Locking { get; private set; }

        // [ThreadStatic] is broken in .net compact framework (Xbox 360)
        // https://www.gavpugh.com/2010/11/26/xnac-%E2%80%93-threadstatic-attribute-is-broken-on-xbox-360/
#if WINDOWS
        [ThreadStatic]
        private static BufferPool<T> threadPool;
#endif

        /// <summary>
        /// Gets the pool associated with this thread.
        /// </summary>
#if !WINDOWS
        public static BufferPool<T> Thread 
        { 
            get { return Locking; } // just return the locking one for now
        }
#else
        public static BufferPool<T> Thread
        {
            get { return threadPool ?? (threadPool = new BufferPool<T>()); }
        }
#endif

        static BufferPools()
        {
            Locking = new LockingBufferPool<T>();
        }
    }
}
