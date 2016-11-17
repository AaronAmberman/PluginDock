using System.Windows;
using System.Windows.Controls;
using Xceed.Wpf.AvalonDock.Controls;

namespace PluginDockUI
{
    /// <summary>Selects the appropriate style.</summary>
    /// <seealso cref="System.Windows.Controls.StyleSelector" />
    public class PluginStyleSelector : StyleSelector
    {
        /// <summary>Gets or sets the layout anchorable style.</summary>
        public Style LayoutAnchorableStyle { get; set; }

        /// <summary>Gets or sets the layout document style.</summary>
        public Style LayoutDocumentStyle { get; set; }

        /// <summary>When overridden in a derived class, returns a <see cref="T:System.Windows.Style" /> based on custom logic.</summary>
        /// <param name="item">The content.</param>
        /// <param name="container">The element to which the style will be applied.</param>
        /// <returns>Returns an application-specific style to apply; otherwise, null.</returns>
        public override Style SelectStyle(object item, DependencyObject container)
        {
            if (container is LayoutAnchorableItem) return LayoutAnchorableStyle;
            if (container is LayoutDocumentItem) return LayoutDocumentStyle;

            return null;
        }
    }
}