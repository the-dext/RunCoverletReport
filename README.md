# RunCoverletReport
A Visual Studio 2019 Extension to run Coverlet and Report Generator

## What is it
The aim of this extension is to make something similar to the functionality provided by the Enterprise edition of Visual Studio, without the cost.
RunCoverletReport is a Visual Studio 2019 extension builds upon the excellet Coverlet and Report Generator that allows you to run dotnet tests with Coverlet to collect code coverage results. These are then used to generate a report using Report Generator, which will be automatically opened in a Visual Studio window.
The Coverlet report will also be used to provide syntax highlighting in Visual Studio C# editor windows.

For more information about coverlet see 
https://github.com/coverlet-coverage/coverlet

For more information about Report Generator see
https://danielpalme.github.io/ReportGenerator/

## Installation

1. Install the Visual Studio extesion. 
After installing the extension there are a couple more steps that are necessary...

2. Install Report Generator as a global tool
Follow this guide https://github.com/danielpalme/ReportGenerator/releases

3. In your unit test projects add the Coverlet.Collector nuget package.

## Usage
Once installed click Tools | Run Code Coverage in Visual Studio.
Dotnet test will then execute your unit tests and after a few moments (provided all of your tests pass) a report generator window will open shoing the ReportGenerator output. 
C# syntax in your .cs files will also be highlighted to indicate your code coverage. 
A red background for lines that are not covered, a green background for lines that are covered.
