using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using PluginDock.Modeling;

namespace PluginDock
{
    /// <summary>Wraps a plug-in type.</summary>
    [DebuggerDisplay("Plug-in: {PluginName} | InstanceAllowance: {PluginInstanceAllowance}")]
    [ExcludeFromCodeCoverage]
    public class PluginModel
    {
        /// <summary>Gets the plug-in control wrapper.</summary>
        public ControlWrapper PluginControlWrapper { get; }

        /// <summary>Gets the plug-in type.</summary>
        public Type PluginType { get; }

        /// <summary>Gets the plug-in instance allowance.</summary>
        public InstanceAllowance PluginInstanceAllowance { get; }

        /// <summary>Gets the name of the plug-in.</summary>
        public string PluginName { get; }

        /// <summary>Initializes a new instance of the <see cref="PluginModel" /> class.</summary>
        /// <param name="pluginType">The plug-in type.</param>
        /// <param name="instanceAllowance">The instance allowance.</param>
        /// <param name="pluginName">Name of the plug-in.</param>
        /// <param name="controlWrapper">The control wrapper.</param>
        public PluginModel(Type pluginType, InstanceAllowance instanceAllowance, string pluginName, ControlWrapper controlWrapper)
        {
            PluginType = pluginType;
            PluginInstanceAllowance = instanceAllowance;
            PluginName = pluginName;
            PluginControlWrapper = controlWrapper;
        }
    }
}