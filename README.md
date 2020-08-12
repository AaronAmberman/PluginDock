# PluginDock
WPF plug-in API/framework that utilizes AvalonDock for visual management.

## Thanks
We want to thank the folks over at [Xceed](https://xceed.com/) for putting together such an amazing product like [AvalonDock](https://avalondock.codeplex.com/).

### Development Overview
There are a few things to using the API and framework. First, your "plug-in" file.

#### Your Plug-in File
```C#
/// <summary>The entry point into the plugin.</summary>
[PluginName("Framework Element Layout Anchor-able Multiple Instance Demo")]
[PluginInstanceAllowance(InstanceAllowance.Multiple)]
[ControlWrapper(ControlWrapper.LayoutAnchorable)]
public class FELAMI : IFrameworkElementPlugin
{
    #region Fields
    private readonly FELAPlugin mainControl = new FELAPlugin();
    #endregion

    #region Properties
    /// <summary>Gets the display name.</summary>
    public string DisplayName => "FELA MI Demo";

    /// <summary>Gets or sets a value indicating whether or not the plug-in is closable.</summary>
    public bool IsClosable { get; set; } = true;

    /// <summary>Gets or sets a value indicating whether or not the plug-in is floatable.</summary>
    public bool IsFloatable { get; set; } = true;

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
```
*Note: FELAMI is short for Framework Element Layout Anchorable Multiple Instance*

This defines your plug-in and how it is utilized in the dock manager. 

You'll notice that there is a `PluginName` attribute and a `DisplayName` property. This is so you can bind a constant name to something like a `MenuItem` (as we do below) and assign a different title to the `DisplayName` which is used for the `LayoutAnchorable` header and the `LayoutDocument` header. So for example if your plug-in has a name of "JobViewer" but you wanted each open instance to be "Job Viewer", "Job Viewer 2", "Job Viewer 3", etc then you can. You just have to adjust the `DisplayName` of each instance.

You'll also notice the `PluginInstanceAllowance` attribute. This tells the plug-in API/framework to only allow one or mutliple.

The last attribute `ControlWrapper` tells the API/framework what type of visual AvalonDock container you want. The only two options are `LayoutAnchorable` and `LayoutDocument`.

#### Your Plug-in User Interface
This will just be a WPF `UserControl` or any `FrameworkElement` you may want to use. We suggest a `UserControl` to keep things simple.

```XAML
<UserControl x:Class="Camelot.Plugins.PluginUserInterfaces.ProjectExplorerPlugin"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006" 
             xmlns:d="http://schemas.microsoft.com/expression/blend/2008" 
             xmlns:local="clr-namespace:Camelot.Plugins.PluginUserInterfaces"
             mc:Ignorable="d" 
             d:DesignHeight="300" d:DesignWidth="300">
    <Grid>
        <Border Background="LightGray" Padding="5">
            <TextBlock Text="Plug-in goodness!" />
        </Border>
    </Grid>
</UserControl>
```

We aren't going to show bindings to our DataContext assigned in the plug-in file above as this is not a MVVM overview. This is because the API/framework doesn't care what you do internally. It's just responsible for loading and instantiating your plug-in(s).

#### Tying it Together in Your Application (Front-end)
The API/framework will not know how you want to use the plug-in references so it's up to you to tell your application what to do. What that statement means in how you want to instantiate them. What user interaction will drive the creation of a plug-in user interface? In our example we used a `MenuItem`. 

Notice the use of the `PluginDockManager`. This is the control responsible for managing plug-in instances at run-time.

```XAML
<Window x:Class="PluginDockWindow.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:PluginDockWindow"
        xmlns:pluginDock="clr-namespace:PluginDockUI;assembly=PluginDockUI"
        xmlns:pluginDockModeling="clr-namespace:PluginDockUI.Modeling;assembly=PluginDockUI"
        mc:Ignorable="d"
        Title="Plug-in Dock" Height="768" Width="1024"
        WindowStartupLocation="CenterScreen"
        Loaded="MainWindow_OnLoaded"
        d:DataContext="{d:DesignInstance {x:Type local:MainWindowViewModel}}">
    <Grid>
        <Grid.RowDefinitions>
            <RowDefinition Height="Auto" />
            <RowDefinition Height="*" />
        </Grid.RowDefinitions>
        <Menu Grid.Row="0">
            <MenuItem Header="File">
                <MenuItem x:Name="exit" Header="Exit" Click="Exit_OnClick" />
            </MenuItem>
            <MenuItem Header="Plug-ins" ItemsSource="{Binding PluginDockManagerViewModel.Plugins}">
                <MenuItem.ItemContainerStyle>
                    <Style TargetType="MenuItem">
                        <Setter Property="Command" Value="{Binding Path=InstantiateCommand}" />
                    </Style>
                </MenuItem.ItemContainerStyle>
                <MenuItem.ItemTemplate>
                    <DataTemplate DataType="{x:Type pluginDockModeling:PluginViewModel}">
                        <TextBlock Text="{Binding PluginName}" />
                    </DataTemplate>
                </MenuItem.ItemTemplate>
            </MenuItem>
        </Menu>
        <pluginDock:PluginDockManager x:Name="pluginDockManager" Grid.Row="1"
                                      DataContext="{Binding PluginDockManagerViewModel}" />
    </Grid>
</Window>
```

#### Tying it Together in Your Application (Back-end)
How and where you choose to load your plug-ins is up to you. For simplicity sake we just used the `LoadedEvent` of the `MainWindow` in the demo.

```C#
try
{
    // you might have a setting or something
    string location = Assembly.GetExecutingAssembly().Location;
    location = location.Substring(0, location.LastIndexOf("\\", StringComparison.OrdinalIgnoreCase) + 1) + "Plugins\\";

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
```

For how the `InitializePluginDockManagerViewModel` method works please see the source code.

#### Tying it Together in Your Application (General)
The thing that is most important to note is the fact that **the API/framework does not manage the existence or creation of your plug-in directory or plug-ins.** It is up to your application to create and manage the existence of the directory and manage the installation, updating or deleting plug-ins.

**A few important things to note.**
- You should NOT make your plug-in directory your application's main directory. That is a very bad idea!
- All non GAC registered dependent assemblies must be included in the plug-in directory with your plug-in. You can exclude assemblies already loaded by main application or other plug-ins as their types will already be available at run-time.
- The `PluginDockManager` doesn't directly support EDA. It requires a `PluginDockManagerViewModel` to be set as its `DataContext`. However it's up to you if you decide to assign the `DataContext` via a binding or set it manually in a code behind file.



## Example Screen Shot
![alt text](https://github.com/DotNetDevCreationist/PluginDock/blob/master/PluginDock.png)