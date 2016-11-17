using System;
using System.Collections.Generic;
using System.Reflection;
using PluginDock.Collections;
using PluginDock.Modeling;

namespace PluginDock.Servicing
{
    /// <summary>Handles the loading of plug-ins.</summary>
    /// <seealso cref="IPluginLoader" />
    public class PluginLoader : IPluginLoader
    {
        #region Properties
        /// <summary>Gets the plug-in reader.</summary>
        public IPluginReader PluginReader { get; }
        #endregion

        #region Constructors
        /// <summary>Initializes a new instance of the <see cref="PluginLoader"/> class.</summary>
        /// <param name="pluginDirectory">The plug-in directory.</param>
        public PluginLoader(string pluginDirectory)
        {
            PluginReader = new PluginReader(pluginDirectory);
        }

        /// <summary>Initializes a new instance of the <see cref="PluginLoader" /> class.</summary>
        /// <param name="pluginReader">The plug-in reader.</param>
        public PluginLoader(IPluginReader pluginReader)
        {
            PluginReader = pluginReader;
        }
        #endregion

        #region Methods
        /// <summary>Instantiates an instance of the specified plug-in type.</summary>
        /// <param name="pluginType">Type of the plug-in.</param>
        /// <returns>An instantiated instance of the specified plug-in type.</returns>
        /// <exception cref="InvalidOperationException">The type provided must be IFrameworkElementPlugin.</exception>
        /// <exception cref="PluginException">An error occurred attempting to instantiate the plug-in type: [T].</exception>
        public IFrameworkElementPlugin InstantiatePluginInstance(Type pluginType)
        {
            try
            {
                var plugin = (IFrameworkElementPlugin)Activator.CreateInstance(pluginType);

                return plugin;
            }
            catch (Exception ex)
            {
                throw new PluginException(
                    FormattableString.Invariant(
                        $"An error occurred attempting to instantiate the plug-in type: {pluginType}."), ex);
            }
        }

        /// <summary>Loads the plug-ins.</summary>
        /// <returns>A collection of the PluginModel's describing each plug-in.</returns>
        /// <exception cref="PluginException">An error occurred attempting to retrieve the plug-in assembly names.</exception>
        /// <exception cref="PluginException">An error occurred attempting to load the plug-in assembly types.</exception>
        /// <exception cref="PluginException">An error occurred attempting to build a plug-in model for type [type].</exception>
        public PluginModelCollection LoadPlugins()
        {
            IReadOnlyList<PluginLocation> plugins = PluginReader.ReadPluginDirectory();
            PluginTypeCollection pluginTypeCollection = PluginReader.LoadPluginAssemblyTypes(plugins);

            PluginModelCollection pluginModelCollection = new PluginModelCollection();

            if (pluginTypeCollection == null || pluginTypeCollection.Count == 0) return pluginModelCollection;

            foreach (Type type in pluginTypeCollection)
            {
                try
                {
                    PluginInstanceAllowanceAttribute pluginInstanceAllowanceAttribute = type.GetCustomAttribute<PluginInstanceAllowanceAttribute>();
                    PluginNameAttribute pluginNameAttribute = type.GetCustomAttribute<PluginNameAttribute>();
                    ControlWrapperAttribute pluginControlWrapperAttribute = type.GetCustomAttribute<ControlWrapperAttribute>();

                    ControlWrapper pluginControlWrapper = pluginControlWrapperAttribute?.ControlWrapper ?? ControlWrapper.LayoutAnchorable;
                    InstanceAllowance pluginInstanceAllowance = pluginInstanceAllowanceAttribute?.InstanceAllowance ?? InstanceAllowance.Single;
                    string pluginName = pluginNameAttribute?.PluginName ?? type.Name;

                    // double check to make sure the PluginName wasn't set to "" or string.Empty
                    if (string.IsNullOrWhiteSpace(pluginName)) pluginName = type.Name;

                    pluginModelCollection.Add(new PluginModel(type, pluginInstanceAllowance, pluginName, pluginControlWrapper));
                }
                catch (Exception ex)
                {
                    throw new PluginException(FormattableString.Invariant($"An error occurred attempting to build a plug-in model for type {type}."), ex);
                }
            }

            return pluginModelCollection;
        }
        #endregion
    }
}