using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace PluginDock.Modeling
{
    /// <summary>An attribute to describe the plug-in name.</summary>
    /// <seealso cref="Attribute" />
    [AttributeUsage(AttributeTargets.Class)]
    [DebuggerDisplay("Plugin name: {PluginName}")]
    [ExcludeFromCodeCoverage]
    public sealed class PluginNameAttribute : Attribute
    {
        /// <summary>Gets or sets the name of the plug-in.</summary>
        public string PluginName { get; }

        /// <summary>Initializes a new instance of the <see cref="PluginNameAttribute"/> class.</summary>
        /// <param name="pluginName">Name of the plug-in.</param>
        public PluginNameAttribute(string pluginName)
        {
            PluginName = pluginName;
        }
    }
}