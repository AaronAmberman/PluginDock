using System.Windows;
using PluginDock.Modeling;

namespace DemoPlugins.FrameworkElementPlugins
{
    /// <summary>The entry point into the plug-in.</summary>
    [PluginName("Framework Element Layout Document Single Instance Demo")]
    [PluginInstanceAllowance(InstanceAllowance.Single)]
    [ControlWrapper(ControlWrapper.LayoutDocument)]
    public class FELDSI : IFrameworkElementPlugin
    {
        #region Fields
        private readonly FELDPlugin mainControl = new FELDPlugin();
        #endregion

        #region Properties
        /// <summary>Gets the display name.</summary>
        public string DisplayName => "FELD SI Demo";

        /// <summary>Gets or sets a value indicating whether or not the plug-in is closable.</summary>
        public bool IsClosable { get; set; } = false;

        /// <summary>Gets or sets a value indicating whether or not the plug-in is floatable.</summary>
        public bool IsFloatable { get; set; } = false;

        /// <summary>Gets the main control.</summary>
        public FrameworkElement MainControl => mainControl;
        #endregion

        #region Methods
        public void Initialize()
        {
            // do initialization stuff
        }

        public void Dispose()
        {
            // do clean up stuff
        }
        #endregion
    }
}