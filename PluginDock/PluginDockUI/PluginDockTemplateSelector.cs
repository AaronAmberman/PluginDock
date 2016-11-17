using System.Windows;
using System.Windows.Controls;
using PluginDockUI.Modeling;

namespace PluginDockUI
{
    /// <summary>Selects the appropriate template.</summary>
    /// <seealso cref="System.Windows.Controls.DataTemplateSelector" />
    public class PluginDockTemplateSelector : DataTemplateSelector
    {
        /// <summary>Gets or sets the framework element template.</summary>
        public DataTemplate FrameworkElementTemplate { get; set; }

        /// <summary>When overridden in a derived class, returns a <see cref="T:System.Windows.DataTemplate" /> based on custom logic.</summary>
        /// <param name="item">The data object for which to select the template.</param>
        /// <param name="container">The data-bound object.</param>
        /// <returns>Returns a <see cref="T:System.Windows.DataTemplate" /> or null. The default value is null.</returns>
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is FrameworkElementPluginViewModel)
                return FrameworkElementTemplate;

            return null;
        }
    }
}