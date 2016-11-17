using System;
using System.Linq;
using System.Windows.Input;
using PluginDock;
using PluginDock.Modeling;
using PluginDock.Servicing;

namespace PluginDockUI.Modeling
{
    /// <summary>The view model the plug-in model.</summary>
    public class PluginViewModel : ViewModelBase<PluginModel>
    {
        private ICommand instantCommand;
        private readonly PluginDockManagerViewModel parentViewModel;

        /// <summary>Gets or sets the plug-in loader.</summary>
        protected IPluginLoader PluginLoader { get; set; }

        /// <summary>Gets the instantiate command.</summary>
        public ICommand InstantiateCommand =>
            instantCommand ?? (instantCommand = new RelayCommand(OnInstantiate));

        /// <summary>Gets the plug-in control wrapper.</summary>
        public ControlWrapper PluginControlWrapper => Model.PluginControlWrapper;

        /// <summary>Gets the plug-in type.</summary>
        public Type PluginType => Model.PluginType;

        /// <summary>Gets the plug-in instance allowance.</summary>
        public InstanceAllowance PluginInstanceAllowance => Model.PluginInstanceAllowance;

        /// <summary>Gets the name of the plug-in.</summary>
        public string PluginName => Model.PluginName;

        /// <summary>Initializes a new instance of the <see cref="PluginViewModel"/> class.</summary>
        /// <param name="model">The model.</param>
        /// <param name="pluginLoader">The plug-in loader.</param>
        /// <param name="pluginDockManagerViewModel">The plug-in dock manager view model.</param>
        public PluginViewModel(PluginModel model, IPluginLoader pluginLoader, PluginDockManagerViewModel pluginDockManagerViewModel)
        {
            parentViewModel = pluginDockManagerViewModel;

            Model = model;
            PluginLoader = pluginLoader;
        }

        /// <summary>Called by the instantiate command.</summary>
        protected virtual void OnInstantiate()
        {
            if (PluginControlWrapper == ControlWrapper.LayoutAnchorable)
            {
                var match = parentViewModel.AnchorablePlugins.FirstOrDefault(p => p.Model.PluginType == PluginType);

                if (match != null && PluginInstanceAllowance == InstanceAllowance.Single) return;

                var plugin = PluginLoader.InstantiatePluginInstance(Model.PluginType);
                var pluginViewModel = new FrameworkElementPluginViewModel(Model, parentViewModel)
                {
                    FrameworkElementPlugin = plugin
                };

                parentViewModel.AnchorablePlugins.Add(pluginViewModel);

                plugin.Initialize();
            }
            else
            {
                var match = parentViewModel.DocumentPlugins.FirstOrDefault(p => p.Model.PluginType == PluginType);

                if (match != null && PluginInstanceAllowance == InstanceAllowance.Single) return;

                var plugin = PluginLoader.InstantiatePluginInstance(Model.PluginType);
                var pluginViewModel = new FrameworkElementPluginViewModel(Model, parentViewModel)
                {
                    FrameworkElementPlugin = plugin
                };

                parentViewModel.DocumentPlugins.Add(pluginViewModel);

                plugin.Initialize();
            }
        }
    }
}