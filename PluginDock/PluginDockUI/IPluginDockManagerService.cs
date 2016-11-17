using PluginDock.Servicing;
using PluginDockUI.Modeling;

namespace PluginDockUI
{
    /// <summary>Describes an object responsible for loading plug-ins and constructing the PluginDockManagerViewModel.</summary>
    public interface IPluginDockManagerService
    {
        /// <summary>Gets the plug-in loader.</summary>
        IPluginLoader PluginLoader { get; }

        /// <summary>Initializes the PluginDockManagerViewModel.</summary>
        /// <returns>The view model for the PluginDockManager.</returns>
        PluginDockManagerViewModel InitializePluginDockManagerViewModel();
    }
}