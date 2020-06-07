# RunCoverletReport
A Visual Studio 2019 Extension to run Coverlet and Report Generator

## What is it
The aim of this extension is to make something similar to the functionality provided by the Enterprise edition of Visual Studio, without the cost.
RunCoverletReport builds upon the excellent Coverlet and Report Generator tools that allow you to collect code coverage results from unit tests.
This extension will run the two tools and then display the report file within visual studio as well as use the Coverlet output to provide syntax highlighting.

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
