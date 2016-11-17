using PluginDockUI;
using PluginDockUI.Modeling;

namespace PluginDockWindow
{
    /// <summary>View model class for the MainWindow.</summary>
    public class MainWindowViewModel : ViewModelBase
    {
        private PluginDockManagerViewModel pluginDockManagerViewModel;

        public IPluginDockManagerService PluginDockManagerService { get; }

        public PluginDockManagerViewModel PluginDockManagerViewModel
        {
            get { return pluginDockManagerViewModel; }
            set { pluginDockManagerViewModel = value; OnPropertyChanged(); }
        }

        public MainWindowViewModel(string location)
        {
            PluginDockManagerService = new PluginDockManagerService(location);
        }

        public MainWindowViewModel(IPluginDockManagerService pluginDockManagerService)
        {
            PluginDockManagerService = pluginDockManagerService;
        }

        public void InitializePluginDockManagerViewModel()
        {
            PluginDockManagerViewModel = PluginDockManagerService.InitializePluginDockManagerViewModel();
        }
    }
}