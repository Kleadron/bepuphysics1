using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace BEPUutilities.ResourceManagement
{
#if XBOX360
    /// <summary>
    /// Contains locking and thread static buffer pools for the specified type.
    /// </summary>
    /// <typeparam name="T">Type of element in the buffers stored in the pools.</typeparam>
    public static class BufferPools<T>
    {
        /// <summary>
        /// Gets a buffer pool for this type which provides thread safe resource acquisition and return.</summary>
        public static LockingBufferPool<T> Locking { get; private set; }

        // NOTE: THIS SHIT BROKEN ON XBOX :(
        //[ThreadStatic]
        //private static BufferPool<T> threadPool;
        static Dictionary<Thread, BufferPool<T>> threadPools;

        /// <summary>
        /// Gets the pool associated with this thread.
        /// </summary>
        public static BufferPool<T> Thread
        {
            //get { return threadPool ?? (threadPool = new BufferPool<T>()); }
            get
            {
                BufferPool<T> pool;
                lock (threadPools)
                {
                    Thread currentThread = System.Threading.Thread.CurrentThread; // this sucks

                    if (!threadPools.TryGetValue(currentThread, out pool))
                    {
                        pool = new BufferPool<T>();
                        threadPools[currentThread] = pool;
                    }
                }
                return pool;
            }
        }

        static BufferPools()
        {
            threadPools = new Dictionary<Thread, BufferPool<T>>();
            Locking = new LockingBufferPool<T>();
        }
    }
#else
    /// <summary>
    /// Contains locking and thread static buffer pools for the specified type.
    /// </summary>
    /// <typeparam name="T">Type of element in the buffers stored in the pools.</typeparam>
    public static class BufferPools<T>
    {
        /// <summary>
        /// Gets a buffer pool for this type which provides thread safe resource acquisition and return.</summary>
        public static LockingBufferPool<T> Locking { get; private set; }

        // NOTE: THIS SHIT BROKEN ON XBOX :(
        [ThreadStatic]
        private static BufferPool<T> threadPool;

        /// <summary>
        /// Gets the pool associated with this thread.
        /// </summary>
        public static BufferPool<T> Thread
        {
            get { return threadPool ?? (threadPool = new BufferPool<T>()); }
        }

        static BufferPools()
        {
            Locking = new LockingBufferPool<T>();
        }
    }
#endif
}
