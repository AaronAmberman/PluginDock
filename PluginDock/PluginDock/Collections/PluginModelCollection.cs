using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using PluginDock.Modeling;

namespace PluginDock.Collections
{
    /// <summary>A specialized collection for plug-ins.</summary>
    /// <seealso cref="PluginBaseCollection{PluginModel}" />
    [DebuggerDisplay("Count : {Count}")]
    [ExcludeFromCodeCoverage]
    public class PluginModelCollection : PluginBaseCollection<PluginModel>
    {
        #region Methods
        /// <summary>Gets the plug-in's control wrapper type.</summary>
        /// <param name="item">The item.</param>
        /// <returns>The plug-ins control wrapper type.</returns>
        /// <exception cref="ArgumentNullException">item cannot be null.</exception>
        public override ControlWrapper GetPluginControlWrapper(PluginModel item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item), "item cannot be null.");

            return item.PluginControlWrapper;
        }

        /// <summary>Returns the name of the plug-in.</summary>
        /// <param name="item">The item.</param>
        /// <returns>The name of the plug-in.</returns>
        /// <exception cref="ArgumentNullException">item cannot be null.</exception>
        public override string GetPluginName(PluginModel item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item), "item cannot be null.");

            return item.PluginName;
        }
        #endregion
    }
}