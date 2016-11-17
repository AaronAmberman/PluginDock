using System.Linq;
using PluginDock.Collections;
using PluginDock.Servicing;
using PluginDockUI.Collections;
using PluginDockUI.Modeling;

namespace PluginDockUI
{
    /// <summary>Service that handles the loading of plug-ins and constructing the view model for the PluginDock.</summary>
    /// <seealso cref="PluginDockUI.IPluginDockManagerService" />
    public class PluginDockManagerService : IPluginDockManagerService
    {
        #region Properties
        /// <summary>Gets the plug-in loader.</summary>
        public IPluginLoader PluginLoader { get; }
        #endregion

        #region Constructors
        /// <summary>Initializes a new instance of the <see cref="PluginDockManagerService"/> class.</summary>
        /// <param name="pluginDirectory">The plug-in directory.</param>
        public PluginDockManagerService(string pluginDirectory)
        {
            PluginLoader = new PluginLoader(pluginDirectory);
        }

        /// <summary>Initializes a new instance of the <see cref="PluginDockManagerService"/> class.</summary>
        /// <param name="pluginLoader">The plug-in loader.</param>
        public PluginDockManagerService(IPluginLoader pluginLoader)
        {
            PluginLoader = pluginLoader;
        }
        #endregion

        #region Methods
        /// <summary>Initializes the PluginDockManagerViewModel.</summary>
        /// <returns>The view model for the PluginDockManager.</returns>
        public PluginDockManagerViewModel InitializePluginDockManagerViewModel()
        {
            PluginDockManagerViewModel viewModel = new PluginDockManagerViewModel(PluginLoader.PluginReader.PluginDirectory);

            PluginModelCollection plugins = PluginLoader.LoadPlugins();

            PluginViewModelCollection pluginViewModels = new PluginViewModelCollection();
            pluginViewModels.AddMany(plugins.Select(p => new PluginViewModel(p, PluginLoader, viewModel)));

            viewModel.Plugins.AddMany(pluginViewModels);

            return viewModel;
        }
        #endregion
    }
}