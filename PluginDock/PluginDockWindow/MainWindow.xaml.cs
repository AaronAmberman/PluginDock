using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows;

namespace PluginDockWindow
{
    /// <summary>A window shell for the plug-in dock.</summary>
    public partial class MainWindow : Window
    {
        /// <summary>Initializes a new instance of the <see cref="MainWindow"/> class.</summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            // this does not have to go in the load logic...put it anywhere, put here for ease of demonstration 
            try
            {
                // you might have a setting or something
                string location = Assembly.GetExecutingAssembly().Location;
                location = location.Substring(0, location.LastIndexOf("\\", StringComparison.OrdinalIgnoreCase) + 1) + "Plugins\\";

                // ensure our plug-in directory exists
                /*
                    The burden of directory management is left up hosting application. The local developers of that
                    project will have a better idea as to how they want their software to handle such tasks.
                */
                Directory.CreateDirectory(location);

                /*
                    This is just to simplify development. Copy demo plug-ins into plug-in directory.
                */
                string demoPluginLocation = location.Replace(@"PluginDockWindow\bin\Debug\Plugins", @"DemoPlugins\bin\Debug");

                List<string> files = Directory.GetFiles(demoPluginLocation).ToList();

                foreach (var file in files)
                {
                    string filename = Path.GetFileName(file);
                    string destinationFilename = Path.Combine(location, filename);

                    File.Copy(file, destinationFilename);
                }

                // sleep for 3 seconds to let the OS finish copying the files and releasing handles
                Thread.Sleep(3000);

                // important things and stuff!!!
                MainWindowViewModel viewModel = new MainWindowViewModel(location);

                // this should be in a try catch block!!!
                viewModel.InitializePluginDockManagerViewModel();

                // MORE important things and stuff!!!
                DataContext = viewModel;
            }
            catch (Exception ex)
            {
                // you'll probably (and hopefully) have some kind of custom error messaging mechanism
                MessageBox.Show($"Something went wrong loading plug-ins.{Environment.NewLine}Exception{ex}");
            }
        }

        private void Exit_OnClick(object sender, RoutedEventArgs e)
        {
            // this could be moved to a command in the view model to better reflect the MVVM design but eh...
            Application.Current.Shutdown();
        }
    }
}