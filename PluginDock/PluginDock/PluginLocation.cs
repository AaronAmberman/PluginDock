using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace PluginDock
{
    /// <summary>Describes a plug-in in terms of location.</summary>
    [DebuggerDisplay("Plug-in location: {AssemblyLocation}")]
    [ExcludeFromCodeCoverage]
    public class PluginLocation
    {
        /// <summary>Gets the assembly location.</summary>
        public string AssemblyLocation { get; }

        /// <summary>Gets the symbols database location.</summary>
        public string SymbolsDatabaseLocation { get; }

        /// <summary>Initializes a new instance of the <see cref="PluginLocation"/> class.</summary>
        /// <param name="assemblyLocation">The assembly location of the plug-in.</param>
        /// <param name="symbolsDatabaseLocation">The symbols database location of the plug-in.</param>
        public PluginLocation(string assemblyLocation, string symbolsDatabaseLocation)
        {
            AssemblyLocation = assemblyLocation;
            SymbolsDatabaseLocation = symbolsDatabaseLocation;
        }
    }
}