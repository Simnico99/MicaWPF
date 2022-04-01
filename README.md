

<h1 align="center">MicaWPF</h1><br />
<div align="center"> 
<img src="./Logo/Logo178x178.png" width="256"/>
    </div>
<div align="center">
<h4>This is a library to make Mica available in WPF because we can't wait for WinUI 3.0 to support it in unpackaged apps.</h4>
    
[![License](https://img.shields.io/github/license/Simnico99/MicaWPF?style=flat)](https://github.com/Simnico99/MicaWPF/blob/main/LICENSE)
[![NuGet](https://img.shields.io/nuget/v/MicaWPF.svg?style=flat&logo=NuGet)](https://www.nuget.org/packages/MicaWPF/latest)
[![NuGet(preview)](https://img.shields.io/nuget/vpre/MicaWPF.svg?style=flat&logo=NuGet)](https://www.nuget.org/packages/MicaWPF/latest/prerelease)
[![NuGet(download)](https://img.shields.io/nuget/dt/MicaWPF.svg?style=flat&logo=NuGet)](https://www.nuget.org/packages/MicaWPF/)
[![CodeFactor](https://img.shields.io/codefactor/grade/github/Simnico99/MicaWPF/main?logo=codefactor&logoColor=%23ffff)](https://www.codefactor.io/repository/github/simnico99/micawpf/overview/main)
</div>

<h2 align="center">Overview</h2>

### Installation
Download via the Nuget package manager.
```nuget
Install-Package MicaWPF
```

# Preview
## Windows 11
Here are the different type of SystemBackdrop you can have:
```xaml
SystemBackdropType="Mica" <!-- This is the default one --> 
```
![Mica_Exemple](https://user-images.githubusercontent.com/80013536/146576610-09cdf07d-0170-4e48-b65d-6612fd7b31fb.png)
```xaml
SystemBackdropType="Tabbed"
```
![Tabbed_Exemple](https://user-images.githubusercontent.com/80013536/146576612-17d9084a-8f4b-4c57-b986-344d5ead40f4.png)
```xaml
SystemBackdropType="Acrylic"
```
![Acrylic_Exemple](https://user-images.githubusercontent.com/80013536/146576613-09eac8d0-44f7-41a4-b92a-4244802a7f18.png)
```xaml
SystemBackdropType="None"
```
![None_Exemple](https://user-images.githubusercontent.com/80013536/146576608-f4300db9-7a45-4bcd-ba13-c79160e2bca8.png)

Also snap layout works with this method:<br/>
![image](https://user-images.githubusercontent.com/80013536/139436498-ab330947-7df3-4c24-a382-3974ef554db2.png)

## Windows 10
Supports falling back to dark or light theme on Windows 10.
![image](https://user-images.githubusercontent.com/80013536/139864645-8a48016b-e369-4c9c-9ca9-73ee7fc10a07.png)<br/>

### Usage

### Recommended
<hr>
The easiest way is to use the custom window:

1. In the code behind your window add those lines
```csharp
    public partial class MainWindow : MicaWindow //<-- Make this a mica window right here
    {
        public MainWindow()
        {
            InitializeComponent();
        }
    }
```

2. In the window xaml add this:
```xaml
<mica:MicaWindow x:Class="WpfDemo.MainWindow" <!-- Add Mica at the start here -->
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:WpfDemo"
        xmlns:mica="clr-namespace:MicaWPF.Controls;assembly=MicaWPF"  <!-- Add MicaWPF Reference here -->
        mc:Ignorable="d"
        Title="MainWindow" 
        Height="450" 
        Width="800">
    <Grid>

    </Grid>
</mica:MicaWindow>  <!-- Don't forget to add Mica at the end here too -->

```


### Compatibility
<hr>
If you already use a custom window you can do this:

1. In the code behind your window add those lines
```csharp
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded; //<-- Add this line
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e) //< --Add this entire method or add to your loaded method.
        {
            this.EnableMica(WindowsTheme.Auto, true); 
        }
    }
```

2. In the window xaml add this:
```xaml
<Window x:Class="WpfDemo.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:WpfDemo"
        mc:Ignorable="d"
        Title="MainWindow" 
        Height="450" 
        Width="800"        
        Background="Transparent"> <!-- It's important to make it transparent -->
    <WindowChrome.WindowChrome> <!-- You can customise this section so it works well with your kind of app. -->
        <WindowChrome 
            CaptionHeight="20"
            ResizeBorderThickness="8"
            CornerRadius="0"
            GlassFrameThickness="-1"
            UseAeroCaptionButtons="True" />
    </WindowChrome.WindowChrome>
    <Grid>

    </Grid>
</Window>

```

![Alt](https://repobeats.axiom.co/api/embed/756130021d85947f6cd1d56b08c1f7b358e5d3a5.svg "Repobeats analytics image")
