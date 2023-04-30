# Run Coverlet Report
A Visual Studio Extension to make showing code coverage easy.

## What is it
The aim of this extension is to make something similar to the functionality provided by the Enterprise edition of Visual Studio, without the cost.
Run Coverlet Report builds upon the excellent Coverlet and Report Generator tools that allow you to collect code coverage results from unit tests.
This extension will run the two tools and then display the report file within Visual Studio as well as use the Coverlet output to provide syntax highlighting.

For more information about coverlet see
https://github.com/coverlet-coverage/coverlet

For more information about Report Generator see
https://danielpalme.github.io/ReportGenerator/

## Important: Visual Studio 2022 Update

The previous version of this extension supports VS2019 but going forwards the extension will target Visual Studio 2022 only.
The VS2022 version of this extension has been given a new product id, which will allow it to be installed alongside the previous VS2019 version.

Unfortunately the testing of two versions of extension is too much for me to do at this time, so the decision was made to focus on just the latest Visual Studio version.
If you are interested in the source code for the VS2019 version, it has been moved into the branch `vs2019`.

## Installation

1. Install the Visual Studio extension (from the extensions menu in Visual Studio). 
Search for 'Run Coverlet Report' or 'Run Coverlet Report 2022' depending on your version of Visual Studio

2. Install Report Generator as a global tool
Follow this guide https://github.com/danielpalme/ReportGenerator/releases

3. In your unit test projects add the Coverlet.MSBuild Nuget package (due to a bug Coverlet.Collector support will be added in future, please follow [temporary instructions](https://github.com/the-dext/RunCoverletReport/issues/1#issuecomment-667349442) in the meantime).

## Usage

1. Configure your projects to collect code coverage by following the Quick-start guide here
https://github.com/coverlet-coverage/coverlet#Quick-Start

2. Configure the integration type used by Run Coverlet Report by setting the integration type that can be found in Tools | Options | Run Coverlet Report.    
By default Coverlet.Collector is used, but if you get error messages when running the coverage report then check the Coverlet packages you are using, if you're using Coverlet.MSBuild instead of Collector then changing this option should allow coverate collection on your solution.
Having a mixture of Coverlet.Collector and Coverlet.MSBuild dependencies in your project isn't supported, this extension will expect one or the other.

To collect coverage and view a coverage report click Tools | Run Code Coverage.  

'dotnet test' will then execute your unit tests and after a few moments **(provided all of your tests pass)** a report generator window will open showing the ReportGenerator output.
C# syntax in your .cs files will also be highlighted to indicate your code coverage.

* Red background = uncovered code.
* Green background = covered code
* Orange background = a part covered line.

![Run Coverlet Report Preview](src/RunCoverletReport/Art/RunCoverletReportPreview.gif)

## Version History

### 2.1.0
Added support for using a run-settings file at solution level with your unit tests.
To turn this on go to `Tools | Options | Run Coverlet Report` and set `Use Run Settings` to true (under 5. Miscellaneous).
Thanks to cdessana for this contribution.

### 2.0.0
*Visual Studio 2022* Onwards

The previous version of this extension supports VS2019 but going forwards the extension will target Visual Studio 2022 only.
The VS2022 version of this extension has been given a new product id, which will allow it to be installed alongside the VS2019 version.

Source code for the VS2019 version is now in the branch `vs2019`

### 1.12
Bug fix for solutions where two or more projects are using linked files. Previously the code attempted to add a line result for the same underlying file multiple times.
Now the most optimistic result for that file will be used to report coverage on that file across all projects.
Thanks to @StingyJack for this fix.

### 1.11
New syntax highlighting options and new layout on the options page.
Borders and Highlights now support more styles (solid, linear and none), and colour options. 

Use a combination of border and highlight styles to create the look that suits your development environment, for example Linear borders
- Border Style = Linear
- Border Linear End Colour = #00FFFFFF (transparent)
- Highlight Style = None

### 1.10
Better support for running projects that have a docker compose file by adding an option to control whether or not nuget packages are restored during the test run.
This was contributed by woodworm83.

### 1.9
Configurable Integration Type - Added an option to switch between relying on Coverlet.Collector (default) and Coverlet.MSBuild.
Change this in Tools | Options | Run Coverlet Report.
By default Coverlet.Collector is used, but if you get error messages when running the coverage report then check the Coverlet packages you are using, if you're using Coverlet.MSBuild instead of Collector then changing this option should allow coverate collection on your solution.
Having a mixture of Coverlet.Collector and Coverlet.MSBuild dependencies in your project isn't supported, this extension will expect one or the other.

### 1.8.1
Bug fix. When assembly pattern has comma characters these needed to be escaped for coverlet to successfully process the pattern.

### 1.8
Added options page (Tools | Options | Run Coverlet Report) to allow user customisation of highlight colours and to specify assemblies & types that should be excluded from code coverage.

* Syntax for hightlight colours is sRGB format (alpha, red, green ,blue) for example #50FFFFFF*
* Syntax for specifying assemblies to exclude is a comma separated set of [assembly-filter]type-filter values. For example the default value of `[*.Tests?]*,[*.UITests?]*` will exclude all types from assemblies with a '.Test' or '.Tests' suffix and assemblies with a '.UITest' or '.UITests' suffix.

### 1.7
Tweaked colours to make them stand out more, on some systems the transparency was too high which caused them to not show up.

### 1.6
First version made public on the Visual Studio Marketplace
