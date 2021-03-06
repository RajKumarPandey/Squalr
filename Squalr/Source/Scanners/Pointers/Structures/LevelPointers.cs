﻿namespace Squalr.Source.Scanners.Pointers.Structures
{
    using SqualrCore.Source.Utils.Extensions;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Structure for organizing all levels of pointer pools that a discovered pointer path may pass through.
    /// </summary>
    internal class LevelPointers
    {
        private PointerPool modulePointerPool;

        /// <summary>
        /// Initializes a new instance of the <see cref="LevelPointers" /> class.
        /// </summary>
        public LevelPointers()
        {
            this.HeapPointerPools = new List<PointerPool>();
        }

        /// <summary>
        /// Gets or sets the list of all pointer pools, including the modules and destination.
        /// </summary>
        public IList<PointerPool> PointerPools
        {
            get
            {
                return new List<PointerPool>(this.HeapPointerPools.PrependIfNotNull(this.ModulePointerPool).AppendIfNotNull(this.DestinationPointerPool));
            }
        }

        /// <summary>
        /// Gets or sets the pointer pool for all modules.
        /// </summary>
        public PointerPool ModulePointerPool { get; set; }

        /// <summary>
        /// Gets or sets the pointer pool containing a single destination address.
        /// </summary>
        public PointerPool DestinationPointerPool { get; set; }

        /// <summary>
        /// Gets the heap pointer pools.
        /// </summary>
        public IList<PointerPool> HeapPointerPools { get; private set; }

        /// <summary>
        /// Gets the non static pointer pools. This is comprised of the heap pointer pools and the destination pointer pool.
        /// </summary>
        public IEnumerable<PointerPool> NonStaticPointerPools
        {
            get
            {
                return this.HeapPointerPools.AppendIfNotNull(this.DestinationPointerPool);
            }
        }

        /// <summary>
        /// Gets the total number of levels, including the module and destination levels.
        /// </summary>
        public Int32 Count
        {
            get
            {
                return this.PointerPools.Count;
            }
        }

        /// <summary>
        /// Adds a new pointer pool level. This will be inserted as the first level.
        /// </summary>
        /// <param name="level">The new pointer level.</param>
        public void AddHeapPointerPool(PointerPool level)
        {
            this.HeapPointerPools.Insert(0, level);
        }
    }
    //// End class
}
//// End namespace