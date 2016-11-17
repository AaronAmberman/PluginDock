using PluginDockUI.Collections;
using Xceed.Wpf.AvalonDock.Themes;

namespace PluginDockUI.Modeling
{
    /// <summary>View model for the PluginDockManager.</summary>
    /// <seealso cref="ViewModelBase" />
    public class PluginDockManagerViewModel : ViewModelBase
    {
        #region Fields
        private Theme theme;
        #endregion

        #region Properties        
        /// <summary>Gets the LayoutAnchorable wrapped plug-ins.</summary>
        public PluginObservableCollection AnchorablePlugins { get; }

        /// <summary>Gets the LayoutDocument wrapped plug-ins.</summary>
        public PluginObservableCollection DocumentPlugins { get; }

        /// <summary>Gets the collection plug-ins.</summary>
        public PluginViewModelCollection Plugins { get; }

        /// <summary>Gets the plug-in directory.</summary>
        public string PluginDirectory { get; }

        /// <summary>Gets or sets the theme.</summary>
        public Theme Theme
        {
            get { return theme; }
            set { theme = value; OnPropertyChanged(); }
        }
        #endregion

        #region Constructors
        /// <summary>Initializes a new instance of the <see cref="PluginDockManagerViewModel"/> class.</summary>
        public PluginDockManagerViewModel(string pluginDirectory)
        {
            /*
                When assigning a theme to anything but the default theme there is a caveat. As a user collapses 
                the side anchorable panels to nothing but their headers, mouses over one, then tries to expand 
                the anchorable region (by dragging the grid splitter), it throws an InvalidOperationException 
                [The visual must be connected to a presentation source.]. This would seem to be an issue with 
                AvalonDock themes. Even if you assign the same generic theme that is the default the error still 
                occurs. 
            */
            //theme = new MetroTheme();

            AnchorablePlugins = new PluginObservableCollection();
            DocumentPlugins = new PluginObservableCollection();
            PluginDirectory = pluginDirectory;
            Plugins = new PluginViewModelCollection();
        }
        #endregion
    }
}