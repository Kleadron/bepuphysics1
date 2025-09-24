#if WINDOWS
using System.Collections.Generic;
using BEPUutilities.DataStructures;
using BEPUutilities;
using Microsoft.Xna.Framework;
using System;

namespace BEPUutilities.ResourceManagement
{
    /// <summary>
    /// Handles allocation and management of commonly used resources.
    /// </summary>
    public static class CommonResources
    {
        [ThreadStatic]
        static UnsafeResourcePool<RawList<RayHit>> SubPoolRayHitList;
        [ThreadStatic]
        static UnsafeResourcePool<RawList<int>> SubPoolIntList;
        [ThreadStatic]
        static UnsafeResourcePool<HashSet<int>> SubPoolIntSet;
        [ThreadStatic]
        static UnsafeResourcePool<RawList<float>> SubPoolFloatList;
        [ThreadStatic]
        static UnsafeResourcePool<RawList<Vector3>> SubPoolVectorList;

        /// <summary>
        /// Retrieves a ray hit list from the resource pool.
        /// </summary>
        /// <returns>Empty ray hit list.</returns>
        public static RawList<RayHit> GetRayHitList()
        {
            if (SubPoolRayHitList == null)
                SubPoolRayHitList = new UnsafeResourcePool<RawList<RayHit>>();
            return SubPoolRayHitList.Take();
        }

        /// <summary>
        /// Returns a resource to the pool.
        /// </summary>
        /// <param name="list">List to return.</param>
        public static void GiveBack(RawList<RayHit> list)
        {
#if DEBUG
            if (SubPoolRayHitList == null)
                throw new Exception("List uninitialized");
#endif
            list.Clear();
            SubPoolRayHitList.GiveBack(list);
        }

        

        /// <summary>
        /// Retrieves a int list from the resource pool.
        /// </summary>
        /// <returns>Empty int list.</returns>
        public static RawList<int> GetIntList()
        {
            if (SubPoolIntList == null)
                SubPoolIntList = new UnsafeResourcePool<RawList<int>>();
            return SubPoolIntList.Take();
        }

        /// <summary>
        /// Returns a resource to the pool.
        /// </summary>
        /// <param name="list">List to return.</param>
        public static void GiveBack(RawList<int> list)
        {
#if DEBUG
            if (SubPoolIntList == null)
                throw new Exception("List uninitialized");
#endif
            list.Clear();
            SubPoolIntList.GiveBack(list);
        }

        /// <summary>
        /// Retrieves a int hash set from the resource pool.
        /// </summary>
        /// <returns>Empty int set.</returns>
        public static HashSet<int> GetIntSet()
        {
            if (SubPoolIntSet == null)
                SubPoolIntSet = new UnsafeResourcePool<HashSet<int>>();
            return SubPoolIntSet.Take();
        }

        /// <summary>
        /// Returns a resource to the pool.
        /// </summary>
        /// <param name="set">Set to return.</param>
        public static void GiveBack(HashSet<int> set)
        {
#if DEBUG
            if (SubPoolIntSet == null)
                throw new Exception("List uninitialized");
#endif
            set.Clear();
            SubPoolIntSet.GiveBack(set);
        }

        /// <summary>
        /// Retrieves a float list from the resource pool.
        /// </summary>
        /// <returns>Empty float list.</returns>
        public static RawList<float> GetFloatList()
        {
            if (SubPoolFloatList == null)
                SubPoolFloatList = new UnsafeResourcePool<RawList<float>>();
            return SubPoolFloatList.Take();
        }

        /// <summary>
        /// Returns a resource to the pool.
        /// </summary>
        /// <param name="list">List to return.</param>
        public static void GiveBack(RawList<float> list)
        {
#if DEBUG
            if (SubPoolFloatList == null)
                throw new Exception("List uninitialized");
#endif
            list.Clear();
            SubPoolFloatList.GiveBack(list);
        }

        /// <summary>
        /// Retrieves a Vector3 list from the resource pool.
        /// </summary>
        /// <returns>Empty Vector3 list.</returns>
        public static RawList<Vector3> GetVectorList()
        {
            if (SubPoolVectorList == null)
                SubPoolVectorList = new UnsafeResourcePool<RawList<Vector3>>();
            return SubPoolVectorList.Take();
        }

        /// <summary>
        /// Returns a resource to the pool.
        /// </summary>
        /// <param name="list">List to return.</param>
        public static void GiveBack(RawList<Vector3> list)
        {
#if DEBUG
            if (SubPoolVectorList == null)
                throw new Exception("List uninitialized");
#endif
            list.Clear();
            SubPoolVectorList.GiveBack(list);
        }

       
    }
}
#endif