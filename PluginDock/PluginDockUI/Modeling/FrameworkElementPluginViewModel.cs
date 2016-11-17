using System.Windows.Input;
using PluginDock;
using PluginDock.Modeling;

namespace PluginDockUI.Modeling
{
    /// <summary>View model for a IFrameworkElementPlugin.</summary>
    /// <seealso cref="ViewModelBase{PluginModel}" />
    public class FrameworkElementPluginViewModel : ViewModelBase<PluginModel>
    {
        private ICommand closeCommand;
        private IFrameworkElementPlugin frameworkElementPlugin;
        private readonly PluginDockManagerViewModel parentViewModel;

        /// <summary>Gets a value indicating whether the plug-in can close.</summary>
        public bool CanClose => frameworkElementPlugin.IsClosable;

        /// <summary>Gets a value indicating whether the plug-in can float.</summary>
        public bool CanFloat => frameworkElementPlugin.IsFloatable;

        /// <summary>Gets the close command.</summary>
        public ICommand CloseCommand => closeCommand ?? (closeCommand = new RelayCommand(OnClose));

        /// <summary>Gets or sets the IFrameworkElementPlugin.</summary>
        public IFrameworkElementPlugin FrameworkElementPlugin
        {
            get { return frameworkElementPlugin; }
            set { frameworkElementPlugin = value; OnPropertyChanged(); }
        }

        /// <summary>Initializes a new instance of the <see cref="FrameworkElementPluginViewModel" /> class.</summary>
        /// <param name="pluginModel">The plug-in model.</param>
        /// <param name="pluginDockManagerViewModel">The plug-in dock manager view model.</param>
        public FrameworkElementPluginViewModel(PluginModel pluginModel, PluginDockManagerViewModel pluginDockManagerViewModel)
        {
            parentViewModel = pluginDockManagerViewModel;

            Model = pluginModel;
        }

        /// <summary>Called by the close command.</summary>
        protected virtual void OnClose()
        {
            if (Model.PluginControlWrapper == ControlWrapper.LayoutAnchorable)
                parentViewModel.AnchorablePlugins.Remove(this);
            else
                parentViewModel.DocumentPlugins.Remove(this);

            if (FrameworkElementPlugin != null)
            {
                FrameworkElementPlugin.Dispose();
                FrameworkElementPlugin = null;
            }
        }
    }
}