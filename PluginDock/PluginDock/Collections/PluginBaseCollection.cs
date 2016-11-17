using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using PluginDock.Modeling;

namespace PluginDock.Collections
{
    /// <summary>Abstract base plug-in collection type.</summary>
    /// <seealso cref="IEnumerable{T}" />
    [DebuggerDisplay("Count: {Count}")]
    [ExcludeFromCodeCoverage]
    public abstract class PluginBaseCollection<T> : IEnumerable<T>
    {
        #region Properties
        /// <summary>Gets or sets the inner item collection.</summary>
        protected IList<T> Items { get; }
        #endregion

        #region Indexers
        /// <summary>Gets the plug-in with the specified plug-in name.</summary>
        /// <param name="pluginName">Name of the plug-in.</param>
        /// <returns>The plug-in the that has the specified name.</returns>
        public T this[string pluginName]
        {
            get { return Items.FirstOrDefault(p => GetPluginName(p).Equals(pluginName, StringComparison.OrdinalIgnoreCase)); }
        }

        /// <summary>Gets the collection of plug-ins of a specified type.</summary>
        /// <param name="view">The type of plug-ins to return.</param>
        /// <returns>A collection of plug-ins.</returns>
        public IEnumerable<T> this[ControlWrapper view]
        {
            get
            {
                return view == ControlWrapper.LayoutAnchorable
                    ? Items.Where(p => GetPluginControlWrapper(p) == ControlWrapper.LayoutAnchorable).ToList()
                    : Items.Where(p => GetPluginControlWrapper(p) == ControlWrapper.LayoutDocument).ToList();
            }
        }
        #endregion

        #region Properties
        /// <summary>Gets the number of items currently in the collection.</summary>
        public int Count => Items.Count;
        #endregion

        #region Constructors
        /// <summary>Initializes a new instance of the <see cref="PluginBaseCollection{T}"/> class.</summary>
        protected PluginBaseCollection()
        {
            Items = new List<T>();
        }
        #endregion

        #region Methods
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>Adds the specified plug-in.</summary>
        /// <param name="plugin">The plug-in.</param>
        public virtual void Add(T plugin)
        {
            Items.Add(plugin);
        }

        /// <summary>Adds multiple plug-ins to the collection at once.</summary>
        /// <param name="pluginsToAdd">The collection of plug-ins to add.</param>
        public virtual void AddMany(IEnumerable<T> pluginsToAdd)
        {
            if (pluginsToAdd == null) return;

            foreach (var plugin in pluginsToAdd)
            {
                Items.Add(plugin);
            }
        }

        /// <summary>Determines whether or not the specified item exists in the collection.</summary>
        /// <param name="item">The item.</param>
        /// <returns>True if contained in the collection, otherwise false.</returns>
        public virtual bool Contains(T item)
        {
            return Items.Contains(item);
        }

        /// <summary>Returns an enumerator that iterates through the collection.</summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        /// <summary>Gets the plug-in's control wrapper type.</summary>
        /// <param name="item">The item.</param>
        /// <returns>The plug-ins control wrapper type.</returns>
        public abstract ControlWrapper GetPluginControlWrapper(T item);

        /// <summary>Returns the name of the plug-in.</summary>
        /// <param name="item">The item.</param>
        /// <returns>The name of the plug-in.</returns>
        public abstract string GetPluginName(T item);

        /// <summary>Removes the specified plug-in.</summary>
        /// <param name="plugin">The plug-in.</param>
        public virtual void Remove(T plugin)
        {
            Items.Remove(plugin);
        }

        /// <summary>Removes multiple plug-ins from the collection at once.</summary>
        /// <param name="pluginsToRemove">The collection of plug-ins to remove.</param>
        public virtual void RemoveMany(IEnumerable<T> pluginsToRemove)
        {
            if (pluginsToRemove == null) return;

            foreach (var plugin in pluginsToRemove)
            {
                Items.Remove(plugin);
            }
        }
        #endregion
    }
}