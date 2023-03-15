<div align="center">

<a href="https://www.nuget.org/packages/MicaWPF/latest">![NuGet](https://img.shields.io/nuget/v/MicaWPF.svg?style=flat&logo=NuGet)</a>
<a href="https://www.nuget.org/packages/MicaWPF/">![NuGet(download)](https://img.shields.io/nuget/dt/MicaWPF.svg?style=flat&logo=NuGet)</a>
<a href="https://www.codefactor.io/repository/github/simnico99/micawpf/overview/main">![CodeFactor](https://img.shields.io/codefactor/grade/github/Simnico99/MicaWPF/main?logo=codefactor&logoColor=%23ffff)</a>
<a href="https://dev.azure.com/ZirconCloud/MicaWPF/_build">![Azure Pipelines](https://dev.azure.com/ZirconCloud/MicaWPF/_apis/build/status/Simnico99.MicaWPF?branchName=main)</a><br/>
<a href="https://github.com/Simnico99/MicaWPF/graphs/contributors">![Contributors](https://img.shields.io/github/contributors/Simnico99/MicaWPF?style=flat)</a>
<a href="https://github.com/Simnico99/MicaWPF/network/members">![Forks](https://img.shields.io/github/forks/Simnico99/MicaWPF?style=flat)</a>
<a href="https://github.com/Simnico99/MicaWPF/stargazers">![Stargazers](https://img.shields.io/github/stars/Simnico99/MicaWPF?style=flat)</a>
<a href="https://github.com/Simnico99/MicaWPF/issues">![Issues](https://img.shields.io/github/issues/Simnico99/MicaWPF?style=flat)</a>
<a href="https://github.com/Simnico99/MicaWPF/blob/main/LICENSE">![License](https://img.shields.io/github/license/Simnico99/MicaWPF?style=flat)</a>

</div>

<!-- PROJECT LOGO -->
<br />
<p align="center">
  <a href="https://github.com/Simnico99/MicaWPF">
    <img src="/Logo/MicaWPFLogo%20-%2080x56.png" alt="Logo" width="80" height="56">
  </a>

  <h3 align="center">MicaWPF</h3>

  <p align="center">
    This is a library to make Mica available in WPF.
    <br />
    <a href="https://github.com/Simnico99/MicaWPF/wiki"><strong>Explore the docs »</strong></a>
    <br />
    <br />
	<a href="https://github.com/Simnico99/MicaWPF/tree/main/src/MicaWPF.Demo">View Demo</a>
    ·
    <a href="https://github.com/Simnico99/MicaWPF/issues/new?template=bug_report.md&title=Bug+Report">Report Bug</a>
    ·
    <a href="https://github.com/Simnico99/MicaWPF/issues/new?template=feature_request.md&title=Feature+Request">Request Feature</a>
  </p>
</p>



<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#installation">Installation</a></li>
        <li><a href="#usage">Usage</a></li>
      </ul>
    </li>
    <li><a href="#roadmap">Roadmap</a></li>
    <li><a href="#contributing">Contributing</a></li>
    <li><a href="#license">License</a></li>
    <li><a href="#contact">Contact</a></li>
    <li><a href="#acknowledgements">Acknowledgements</a></li>
  </ol>
</details>



<!-- ABOUT THE PROJECT -->
## About The Project

The project aim to mimic Windows 11 Mica Behaviors in a WPF application since WPF is a well established framework and also it aims to gracefully revert to a Windows 10 style that is kinda similar to the Dark and Light mode of Windows 11 to keep inline with the style.

Windows 11<br/>
![Mica_Exemple](https://user-images.githubusercontent.com/80013536/146576610-09cdf07d-0170-4e48-b65d-6612fd7b31fb.png)

Windows 10<br/>
Supports falling back to dark or light theme on Windows 10.
![image](https://user-images.githubusercontent.com/80013536/139864645-8a48016b-e369-4c9c-9ca9-73ee7fc10a07.png)<br/>

Here are some exemple controls:
![image](https://user-images.githubusercontent.com/80013536/169356344-caacaa6d-5ee6-447b-b3b5-35fca1a8939b.png)
![74d755e6e4ae5fddb0b5fe64d2869e81](https://user-images.githubusercontent.com/80013536/169356493-95e8d389-815a-4227-a6a6-7a422334f627.gif)
![42cbe5065b53c4e8369f2f4bbeb1311a](https://user-images.githubusercontent.com/80013536/169356743-a4b899c7-ba99-40c9-a5aa-823a5942fd3b.gif)
![image](https://user-images.githubusercontent.com/80013536/169356891-4b5a2221-64e2-4522-ba1e-a2bae9f04b40.png)


### Built With

* [MicaWPFRuntimeComponent](https://github.com/Simnico99/MicaWPFRuntimeComponent)


<!-- GETTING STARTED -->
## Getting Started

To get a local copy up and running follow these simple steps.

### Installation

#### NuGet
1. Download via the Nuget package manager or use the NuGet Command line.
   ```sh
   Install-Package MicaWPF
   ```
2. If you have used the NuGet Command line restore de packages.
   ```sh
   nuget restore MicaWPF.sln
   ```

#### Using source
1. Clone the repo.
   ```sh
   git clone https://github.com/Simnico99/MicaWPF.git
   ```
2. Restore NuGet packages.
   ```sh
   nuget restore MicaWPF.sln
   ```
3. Add the project in your project reference.


<!-- USAGE EXAMPLES -->
### Usage

1. To start Change the `<Window><Window/>` for `<controls:MicaWindow></controls:MicaWindow>`.
2. Add the namespace by adding `xmlns:controls="clr-namespace:MicaWPF.Controls;assembly=MicaWPF"`.

Here is an exemple:
```XAML
<controls:MicaWindow  
        x:Class="MicaWPF.DesktopApp.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:local="clr-namespace:MicaWPF.DesktopApp"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:controls="clr-namespace:MicaWPF.Controls;assembly=MicaWPF"
        mc:Ignorable="d"
        Title="MainWindow" 
        Height="450" 
        Width="800" >
    <Grid>

    </Grid>
</controls:MicaWindow>
```

3. You need to add `<mica:ThemeDictionary Theme="Dark" />` to your App.xaml resources.

Here is an exemple:
```xaml
<Application
    x:Class="MicaWPF.DesktopApp.App"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:mica="clr-namespace:MicaWPF.Styles;assembly=MicaWPF"
    StartupUri="MainWindow.xaml">
    <Application.Resources>
        <ResourceDictionary>
            <ResourceDictionary.MergedDictionaries>
                <mica:ThemeDictionary Theme="Auto" /> <!-- And Here (You can change to Light or Dark here) -->
            </ResourceDictionary.MergedDictionaries>
        </ResourceDictionary>
    </Application.Resources>
</Application>
```

Now get into your Window code:
1. Add the namespace `using MicaWPF.Controls;`.
2. Change the Window inherited class to `MicaWindow`.

Here is an exemple of what it might look like using .NET7:
```CSharp
using MicaWPF.Controls;

namespace MicaWPF.DesktopApp;

public partial class MainWindow : MicaWindow
{
    public MainWindow()
    {
        InitializeComponent();
    }
}

```

#### Note For .Net5.0, .Net6.0 and .Net7.0
You will need to change your CSPROJ to include the windows build after the netx.0-windows.
Here is an exemple using .Net7.0 just change the net7 for net5 or net6 for the other versions.
```Xaml
<TargetFramework>net7.0-windows10.0.19041.0</TargetFramework>
<TargetPlatformMinVersion>7.0</TargetPlatformMinVersion>
```

_For more examples, please refer to the [Documentation](https://github.com/Simnico99/MicaWPF/wiki)_



<!-- ROADMAP -->
## Roadmap

See the [open issues](https://github.com/Simnico99/MicaWPF/issues) for a list of proposed features (and known issues).



<!-- CONTRIBUTING -->
## Contributing

Contributions are what make the open source community such an amazing place to be learn, inspire, and create. Any contributions you make are **greatly appreciated**.

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## FAQ

### Q: The title bar buttons are weird, how do I fix?
A: Apply TitleBarType="WinUI" like this:
```
<mica:MicaWindow [your code...]
TitleBarType="WinUI"
>
[other code...]
</mica:MicaWindow>
```
<!-- LICENSE -->
## License

Distributed under the MIT License. See `LICENSE` for more information.



<!-- CONTACT -->
## Contact

Simnico99 - [@TheSimnico99](https://twitter.com/TheSimnico99)

Project Link: [https://github.com/Simnico99](https://github.com/Simnico99)



<!-- ACKNOWLEDGEMENTS -->
## Acknowledgements

* [Fluent UI System Icons](https://github.com/microsoft/fluentui-system-icons)
* [Best README template](https://github.com/othneildrew/Best-README-Template)

![Alt](https://repobeats.axiom.co/api/embed/756130021d85947f6cd1d56b08c1f7b358e5d3a5.svg "Repobeats analytics image")
