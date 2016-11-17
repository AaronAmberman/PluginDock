using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using PluginDock.Collections;
using PluginDock.Modeling;
using PluginDockUI.Modeling;

namespace PluginDockUI.Collections
{
    /// <summary>A specialized collection for plug-in view models.</summary>
    /// <seealso cref="PluginBaseCollection{PluginModel}" />
    [DebuggerDisplay("Count : {Count}")]
    [ExcludeFromCodeCoverage]
    public class PluginViewModelCollection : PluginBaseCollection<PluginViewModel>
    {
        #region Methods
        /// <summary>Gets the plug-in's control wrapper type.</summary>
        /// <param name="item">The item.</param>
        /// <returns>The plug-ins control wrapper type.</returns>
        /// <exception cref="ArgumentNullException">item cannot be null.</exception>
        public override ControlWrapper GetPluginControlWrapper(PluginViewModel item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item), Properties.Resources.ItemCannotBeNull);

            return item.PluginControlWrapper;
        }

        /// <summary>Returns the name of the plug-in.</summary>
        /// <param name="item">The item.</param>
        /// <returns>The name of the plug-in.</returns>
        /// <exception cref="ArgumentNullException">item cannot be null.</exception>
        public override string GetPluginName(PluginViewModel item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item), Properties.Resources.ItemCannotBeNull);

            return item.PluginName;
        }
        #endregion
    }
}