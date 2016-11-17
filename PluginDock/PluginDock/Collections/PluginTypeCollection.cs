using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using PluginDock.Modeling;

namespace PluginDock.Collections
{
    /// <summary>A specialized collection for plug-ins.</summary>
    /// <seealso cref="PluginBaseCollection{Type}" />
    [DebuggerDisplay("Count : {Count}")]
    [ExcludeFromCodeCoverage]
    public class PluginTypeCollection : PluginBaseCollection<Type>
    {
        #region Methods
        /// <summary>Gets the plug-in's control wrapper type.</summary>
        /// <param name="item">The item.</param>
        /// <returns>The plug-ins control wrapper type.</returns>
        /// <exception cref="ArgumentNullException">item cannot be null.</exception>
        public override ControlWrapper GetPluginControlWrapper(Type item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item), "item cannot be null.");

            try
            {
                return item.GetCustomAttribute<ControlWrapperAttribute>()?.ControlWrapper ?? ControlWrapper.LayoutAnchorable;
            }
            catch
            {
                return ControlWrapper.LayoutAnchorable;
            }
        }

        /// <summary>Returns the name of the plug-in.</summary>
        /// <param name="item">The item.</param>
        /// <returns>The name of the plug-in.</returns>
        /// <exception cref="ArgumentNullException">item cannot be null.</exception>
        public override string GetPluginName(Type item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item), "item cannot be null.");

            return item.Name;
        }
        #endregion
    }
}