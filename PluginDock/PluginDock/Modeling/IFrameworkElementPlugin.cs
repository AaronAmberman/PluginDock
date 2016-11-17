using System;
using System.Windows;

namespace PluginDock.Modeling
{
    /// <summary>Describes a plug-in that has a FrameworkElement for a main control.</summary>
    public interface IFrameworkElementPlugin : IDisposable
    {
        /// <summary>Gets the display name (used as the title of TabItems and ToolWindows).</summary>
        string DisplayName { get; }

        /// <summary>Gets or sets a value indicating whether or not the plug-in is closable.</summary>
        bool IsClosable { get; set; }

        /// <summary>Gets or sets a value indicating whether the plug-in is floatable.</summary>
        bool IsFloatable { get; set; }

        /// <summary>Gets the main control.</summary>
        FrameworkElement MainControl { get; }

        /// <summary>Initializes the plug-in.</summary>
        void Initialize();
    }
}