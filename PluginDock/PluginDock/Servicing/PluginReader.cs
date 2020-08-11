using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using PluginDock.Collections;
using PluginDock.Modeling;

namespace PluginDock.Servicing
{
    /// <summary>Handles the reading of plug-ins.</summary>
    /// <seealso cref="IPluginReader" />
    public class PluginReader : IPluginReader
    {
        #region Fields
        private readonly string pluginLocation;
        #endregion

        #region Properties
        /// <summary>Gets the plug-in directory.</summary>
        public string PluginDirectory => pluginLocation;
        #endregion

        #region Constructors
        /// <summary>Initializes a new instance of the <see cref="PluginReader"/> class.</summary>
        /// <param name="pluginDirectory">The plug-in directory.</param>
        public PluginReader(string pluginDirectory)
        {
            pluginLocation = pluginDirectory;
        }
        #endregion

        #region Methods
        /// <summary>Loads the plug-in assembly types.</summary>
        /// <param name="pluginLocations">The plug-in locations.</param>
        /// <returns>A collection of the plug-in assembly types.</returns>
        /// <exception cref="PluginException">An error occurred attempting to load the plug-in assembly types.</exception>
        public PluginTypeCollection LoadPluginAssemblyTypes(IReadOnlyList<PluginLocation> pluginLocations)
        {
            PluginTypeCollection pluginTypeCollection = new PluginTypeCollection();

            if (pluginLocations == null || pluginLocations.Count == 0) return pluginTypeCollection;

            foreach (PluginLocation location in pluginLocations)
            {
                try
                {
                    string pluginName = location.AssemblyLocation;
                    string symbolDatabaseName = location.SymbolsDatabaseLocation;

                    // read assembly information
                    Assembly pluginAssembly;

                    if (string.IsNullOrWhiteSpace(symbolDatabaseName))
                        pluginAssembly = Assembly.Load(pluginName);
                    else
                    {
                        byte[] pluginBytes = File.ReadAllBytes(pluginName);
                        byte[] symbolDatabaseBytes = File.ReadAllBytes(symbolDatabaseName);

                        pluginAssembly = Assembly.Load(pluginBytes, symbolDatabaseBytes);
                    }

                    // load referenced assemblies...we don't need to do anything with it other than load it into the type universe
                    List<AssemblyName> referecnedAssemblies = pluginAssembly.GetReferencedAssemblies().ToList();

                    foreach (AssemblyName referecnedAssembly in referecnedAssemblies)
                        Assembly.Load(referecnedAssembly);

                    // now that we have our assembly lets get types that are a plug-in type

                    List<Type> frameworkElementPlugins =
                        pluginAssembly.GetTypes().Where(type => typeof(IFrameworkElementPlugin).IsAssignableFrom(type)).ToList();

                    pluginTypeCollection.AddMany(frameworkElementPlugins);
                }
                catch (Exception ex)
                {
                    throw new PluginException(
                        FormattableString.Invariant(
                            $"An error occurred attempting to load the plug-in assembly types for plug-in {location.AssemblyLocation}."), ex);
                }
            }

            return pluginTypeCollection;
        }

        /// <summary>Reads the plug-in directory.</summary>
        /// <returns>A collection of PluginLocations describing each plug-in.</returns>
        /// <exception cref="PluginException">An error occurred attempting to retrieve the plug-in assembly names.</exception>
        public IReadOnlyList<PluginLocation> ReadPluginDirectory()
        {
            try
            {
                List<PluginLocation> plugins = new List<PluginLocation>();

                var pluginNames = Directory.GetFiles(pluginLocation, "*.dll").ToList();
                pluginNames.AddRange(GetAllSubFileTypes(pluginLocation, "*.dll"));

                var symbolDatabases = Directory.GetFiles(pluginLocation, "*.pdb").ToList();
                symbolDatabases.AddRange(GetAllSubFileTypes(pluginLocation, "*.pdb"));

                foreach (string pluginNameWithExtension in pluginNames)
                {
                    string pluginNameWithoutExtension = pluginNameWithExtension.Replace(".dll", "");
                    string pluginName = pluginNameWithoutExtension.Substring(pluginNameWithoutExtension.LastIndexOf('\\') + 1);

                    // grab matching symbol database name...if one exists
                    List<string> symbolDatabasesWithoutExtensions = symbolDatabases.Select(sd => sd.Replace(".pdb", "")).ToList();

                    string symbolDatabaseWithoutExtension =
                        symbolDatabasesWithoutExtensions.FirstOrDefault(
                            sd => sd.Equals(pluginNameWithoutExtension, StringComparison.OrdinalIgnoreCase)) ?? string.Empty;

                    string symbolDatabaseWithExtension = !string.IsNullOrWhiteSpace(symbolDatabaseWithoutExtension)
                        ? symbolDatabases.First(sd => sd.StartsWith(symbolDatabaseWithoutExtension, StringComparison.OrdinalIgnoreCase)) : string.Empty;

                    string symbolDatabaseName = !string.IsNullOrWhiteSpace(symbolDatabaseWithoutExtension)
                        ? symbolDatabaseWithoutExtension.Substring(symbolDatabaseWithoutExtension.LastIndexOf('\\') + 1)
                        : string.Empty;

                    // get combination of plug-in assembly name and symbol database name (or empty string if one does not exist)
                    plugins.Add(new PluginLocation(pluginNameWithExtension,
                        pluginName.Equals(symbolDatabaseName, StringComparison.OrdinalIgnoreCase) ? symbolDatabaseWithExtension : string.Empty));
                }

                return plugins;
            }
            catch (Exception ex)
            {
                throw new PluginException("An error occurred attempting to retrieve the plug-in assembly names.", ex);
            }
        }

        private IList<string> GetAllSubFileTypes(string path, string searchPattern)
        {
            List<string> files = Directory.GetFiles(path, searchPattern).ToList();

            // get our sub-directories matching files

            string[] subDirs = Directory.GetDirectories(path);

            foreach (string dir in subDirs)
            {
                List<string> subFiles = GetAllSubFileTypes(dir, searchPattern).ToList();

                files.AddRange(subFiles);
            }

            return files;
        }
        #endregion
    }
}