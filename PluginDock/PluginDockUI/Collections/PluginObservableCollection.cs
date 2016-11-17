using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using PluginDock.Modeling;
using PluginDockUI.Modeling;
using PluginDockUI.Properties;

namespace PluginDockUI.Collections
{
    /// <summary>Special customized observable collection for plug-ins.</summary>
    /// <seealso cref="ObservableCollection{T}" />
    [ExcludeFromCodeCoverage]
    public class PluginObservableCollection : ObservableCollection<FrameworkElementPluginViewModel>
    {
        #region Indexers
        /// <summary>
        /// Gets the plug-in with the specified plug-in name. If DisplayName is desired 
        /// then it will have to be done on all items of a specific type of plug-in.
        /// </summary>
        /// <param name="pluginName">Name of the plug-in.</param>
        /// <returns>The collection of plug-ins that have the specified name.</returns>
        public IEnumerable<FrameworkElementPluginViewModel> this[string pluginName]
        {
            get
            {
                List<FrameworkElementPluginViewModel> frameworkElementPluginViewModels =
                    Items
                        .Where(viewModel =>
                            viewModel.Model.PluginName.Equals(pluginName, StringComparison.OrdinalIgnoreCase))
                        .ToList();

                // return both types of view models that have a matching name
                return frameworkElementPluginViewModels;
            }
        }

        /// <summary>Gets the <see cref="IEnumerable{FrameworkElementPluginViewModel}"/> with the specified control wrapper.</summary>
        /// <param name="controlWrapper">The control wrapper.</param>
        /// <returns>A collection of plug-in view models that have the specified control wrapper.</returns>
        public IEnumerable<FrameworkElementPluginViewModel> this[ControlWrapper controlWrapper]
        {
            get
            {
                List<FrameworkElementPluginViewModel> frameworkElementPluginViewModels =
                    Items
                        .Where(viewModel => viewModel.Model.PluginControlWrapper == controlWrapper)
                        .ToList();

                // return both types of view models that have the appropriate control wrapper type
                return frameworkElementPluginViewModels;
            }
        }
        #endregion

        #region Methods
        /// <summary>Adds multiple items to the collection at once.</summary>
        /// <param name="itemsToAdd">The items to add.</param>
        public virtual void AddMany(IEnumerable<FrameworkElementPluginViewModel> itemsToAdd)
        {
            if (itemsToAdd == null)
                throw new ArgumentNullException(nameof(itemsToAdd), Resources.ItemsToAdd_CannotBeNull);

            if (!itemsToAdd.Any()) return;

            CheckReentrancy();

            foreach (var item in itemsToAdd)
                Items.Add(item);

            OnPropertyChanged(new PropertyChangedEventArgs("Count"));
            OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));

            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, itemsToAdd.ToList()));
        }

        /// <summary>Adds multiple items to the collection at once.</summary>
        /// <param name="itemsToRemove">The items to add.</param>
        public virtual void RemoveMany(IEnumerable<FrameworkElementPluginViewModel> itemsToRemove)
        {
            if (itemsToRemove == null)
                throw new ArgumentNullException(nameof(itemsToRemove), Resources.ItemsToRemove_CannotBeNull);

            if (!itemsToRemove.Any()) return;

            CheckReentrancy();

            foreach (var item in itemsToRemove)
                Items.Remove(item);

            OnPropertyChanged(new PropertyChangedEventArgs("Count"));
            OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));

            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, itemsToRemove.ToList()));
        }
        #endregion
    }
}