using System.Collections.Generic;
using PluginDock.Collections;

namespace PluginDock.Servicing
{
    /// <summary>Describes an object responsible for reading the plug-ins.</summary>
    public interface IPluginReader
    {
        /// <summary>Gets the plug-in directory.</summary>
        string PluginDirectory { get; }

        /// <summary>Loads the plug-in assembly types.</summary>
        /// <param name="pluginLocations">The plug-in locations.</param>
        /// <returns>A collection of the plug-in assembly types.</returns>
        PluginTypeCollection LoadPluginAssemblyTypes(IReadOnlyList<PluginLocation> pluginLocations);

        /// <summary>Reads the plug-in directory.</summary>
        IReadOnlyList<PluginLocation> ReadPluginDirectory();
    }
}