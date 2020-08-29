# Run Coverlet Report
A Visual Studio 2019 Extension to make showing code coverage easy.

## What is it
The aim of this extension is to make something similar to the functionality provided by the Enterprise edition of Visual Studio, without the cost.
Run Coverlet Report builds upon the excellent Coverlet and Report Generator tools that allow you to collect code coverage results from unit tests.
This extension will run the two tools and then display the report file within visual studio as well as use the Coverlet output to provide syntax highlighting.

For more information about coverlet see
https://github.com/coverlet-coverage/coverlet

For more information about Report Generator see
https://danielpalme.github.io/ReportGenerator/

## Installation

1. Install the Visual Studio extension (from the extensions menu in Visual Studio)

2. Install Report Generator as a global tool
Follow this guide https://github.com/danielpalme/ReportGenerator/releases

3. In your unit test projects add the Coverlet.Collector nuget package.

## Usage

1. Configure your projects to collect code coverage by following the Quickstart guide here
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
