using System;
using PluginDock.Collections;
using PluginDock.Modeling;

namespace PluginDock.Servicing
{
    /// <summary>Describes an object responsible for reading the plug-ins.</summary>
    public interface IPluginLoader
    {
        /// <summary>Gets the plug-in reader.</summary>
        IPluginReader PluginReader { get; }

        /// <summary>Instantiates an instance of the specified plug-in type.</summary>
        /// <param name="pluginType">Type of the plug-in.</param>
        /// <returns>An instantiated instance of the IFrameworkElementPlugin type.</returns>
        IFrameworkElementPlugin InstantiatePluginInstance(Type pluginType);

        /// <summary>Loads the plug-ins.</summary>
        /// <returns>A collection of the PluginModel's describing each plug-in.</returns>
        PluginModelCollection LoadPlugins();
    }
}