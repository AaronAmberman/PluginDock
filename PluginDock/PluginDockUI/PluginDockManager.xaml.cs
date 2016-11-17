using System.Windows;
using System.Windows.Controls;

namespace PluginDockUI
{
    /// <summary>The main plug-in dock UI control.</summary>
    public partial class PluginDockManager : UserControl
    {
        #region Properties
        /// <summary>The layout item container style selector property.</summary>
        public static readonly DependencyProperty LayoutItemContainerStyleSelectorProperty = DependencyProperty.Register(
            "LayoutItemContainerStyleSelector", typeof(StyleSelector), typeof(PluginDockManager), new PropertyMetadata(default(StyleSelector)));

        /// <summary>Gets or sets the layout item container style selector.</summary>
        public StyleSelector LayoutItemContainerStyleSelector
        {
            get { return (StyleSelector)GetValue(LayoutItemContainerStyleSelectorProperty); }
            set { SetValue(LayoutItemContainerStyleSelectorProperty, value); }
        }

        /// <summary>The layout item template selector property.</summary>
        public static readonly DependencyProperty LayoutItemTemplateSelectorProperty = DependencyProperty.Register(
            "LayoutItemTemplateSelector", typeof(DataTemplateSelector), typeof(PluginDockManager), new PropertyMetadata(default(DataTemplateSelector)));

        /// <summary>Gets or sets the layout item template selector.</summary>
        public DataTemplateSelector LayoutItemTemplateSelector
        {
            get { return (DataTemplateSelector)GetValue(LayoutItemTemplateSelectorProperty); }
            set { SetValue(LayoutItemTemplateSelectorProperty, value); }
        }
        #endregion

        #region Constructors
        /// <summary>Initializes a new instance of the <see cref="PluginDockManager"/> class.</summary>
        public PluginDockManager()
        {
            InitializeComponent();
        }
        #endregion

        #region Event Handlers
        private void PluginDockManager_OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            //// leave if in design mode
            //if (DesignerProperties.GetIsInDesignMode(this)) return;

            //// validate view model
            //var viewModel = e.NewValue as PluginDockManagerViewModel;

            //if (viewModel == null)
            //    throw new ArgumentException("The DataContext must be of type PluginDockManagerViewModel.");
        }
        #endregion
    }
}