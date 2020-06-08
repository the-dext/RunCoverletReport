using FluentAssertions;
using RunCoverletReport.CoverageResults.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RunCoverletReport.Tests.CoverageResults
{
    public class CoverageReaderTests
    {
        [Fact]
        public void CanParseCoverageFile()
        {
            // Not a typical unit test but it makes sense to use an example real cobertura xml file and parse it, then assert everything that is expected
            // matches what is returned.
            // start off by asserting that the filename and class results are available, then assert the contents of each class result.
            
            // arrange
            string filename = "example.coverage.cobertura.xml";
            var sut = new CoverageReader();

            // act
            var coverageResults = sut.ReadFile(filename);

            // Assert
            coverageResults.FileName.Should().Be(filename);
            coverageResults.ClassResults.Should().NotBeNull();
            coverageResults.ClassResults.Count.Should().Be(3);

            var calendarCoverage = coverageResults.ClassResults["Calendar\\Calendar.cs"];
            calendarCoverage.Should().NotBeNull();
            calendarCoverage.FileName.Should().Be("Calendar\\Calendar.cs");

            var calculatorCoverage = coverageResults.ClassResults["Calculator\\Calculator.cs"];
            calculatorCoverage.Should().NotBeNull();
            calculatorCoverage.FileName.Should().Be("Calculator\\Calculator.cs");

            var multiplierCoverage = coverageResults.ClassResults["Calculator\\Multiplier.cs"];
            multiplierCoverage.Should().NotBeNull();
            multiplierCoverage.FileName.Should().Be("Calculator\\Multiplier.cs");

            this.AssertCalendarResults(calendarCoverage);
            this.AssertCalculatorResults(calculatorCoverage);
            this.AssertMultiplierResults(multiplierCoverage);
        }

        private void AssertMultiplierResults(ClassResult multiplierCoverage)
        {
            /*
             * Expected coverage 
                <lines>
                    <line number="10" hits="0" branch="False" />
                    <line number="11" hits="0" branch="True" condition-coverage="0% (0/2)">
                      <conditions>
                        <condition number="7" type="jump" coverage="0%" />
                      </conditions>
                    </line>
                    <line number="12" hits="0" branch="False" />
                    <line number="13" hits="0" branch="False" />
                    <line number="15" hits="0" branch="True" condition-coverage="0% (0/2)">
                      <conditions>
                        <condition number="20" type="jump" coverage="0%" />
                      </conditions>
                    </line>
                    <line number="16" hits="0" branch="False" />
                    <line number="17" hits="0" branch="False" />
                    <line number="20" hits="0" branch="False" />
                    <line number="21" hits="0" branch="False" />
                    <line number="23" hits="0" branch="False" />
                </lines> 
             */

            multiplierCoverage.LineResults.Count.Should().Be(10);

            multiplierCoverage.LineResults[10].Should().BeEquivalentTo(new { LineNumber = 10, Hits = 0, Branch = false });
            multiplierCoverage.LineResults[11].Should().BeEquivalentTo(new { LineNumber = 11, Hits = 0, Branch = true });
            multiplierCoverage.LineResults[12].Should().BeEquivalentTo(new { LineNumber = 12, Hits = 0, Branch = false });
            multiplierCoverage.LineResults[13].Should().BeEquivalentTo(new { LineNumber = 13, Hits = 0, Branch = false });
            multiplierCoverage.LineResults[15].Should().BeEquivalentTo(new { LineNumber = 15, Hits = 0, Branch = true });
            multiplierCoverage.LineResults[16].Should().BeEquivalentTo(new { LineNumber = 16, Hits = 0, Branch = false });
            multiplierCoverage.LineResults[17].Should().BeEquivalentTo(new { LineNumber = 17, Hits = 0, Branch = false });
            multiplierCoverage.LineResults[20].Should().BeEquivalentTo(new { LineNumber = 20, Hits = 0, Branch = false });
            multiplierCoverage.LineResults[21].Should().BeEquivalentTo(new { LineNumber = 21, Hits = 0, Branch = false });
            multiplierCoverage.LineResults[23].Should().BeEquivalentTo(new { LineNumber = 23, Hits = 0, Branch = false });
        }

        private void AssertCalculatorResults(ClassResult calculatorCoverage)
        {
            /*
             * Expected coverage 
            <lines>
                <line number="15" hits="2" branch="False" />
                <line number="16" hits="2" branch="False" />
                <line number="17" hits="2" branch="False" />
                <line number="18" hits="2" branch="False" />
                <line number="21" hits="0" branch="False" />
                <line number="22" hits="0" branch="False" />
                <line number="23" hits="0" branch="False" />
                <line number="24" hits="0" branch="False" />
                <line number="27" hits="0" branch="False" />
                <line number="28" hits="0" branch="False" />
                <line number="29" hits="0" branch="False" />
                <line number="32" hits="0" branch="False" />
                <line number="33" hits="0" branch="False" />
                <line number="34" hits="0" branch="False" />
                <line number="9" hits="1" branch="False" />
                <line number="10" hits="1" branch="False" />
                <line number="11" hits="1" branch="False" />
                <line number="12" hits="1" branch="False" />
            </lines> 
             * 
             */
            calculatorCoverage.LineResults.Count.Should().Be(18);

            calculatorCoverage.LineResults[15].Should().BeEquivalentTo(new { LineNumber = 15, Hits = 2, Branch = false });
            calculatorCoverage.LineResults[16].Should().BeEquivalentTo(new { LineNumber = 16, Hits = 2, Branch = false });
            calculatorCoverage.LineResults[17].Should().BeEquivalentTo(new { LineNumber = 17, Hits = 2, Branch = false });
            calculatorCoverage.LineResults[18].Should().BeEquivalentTo(new { LineNumber = 18, Hits = 2, Branch = false });
            calculatorCoverage.LineResults[21].Should().BeEquivalentTo(new { LineNumber = 21, Hits = 0, Branch = false });
            calculatorCoverage.LineResults[22].Should().BeEquivalentTo(new { LineNumber = 22, Hits = 0, Branch = false });
            calculatorCoverage.LineResults[23].Should().BeEquivalentTo(new { LineNumber = 23, Hits = 0, Branch = false });
            calculatorCoverage.LineResults[24].Should().BeEquivalentTo(new { LineNumber = 24, Hits = 0, Branch = false });
            calculatorCoverage.LineResults[27].Should().BeEquivalentTo(new { LineNumber = 27, Hits = 0, Branch = false });
            calculatorCoverage.LineResults[28].Should().BeEquivalentTo(new { LineNumber = 28, Hits = 0, Branch = false });
            calculatorCoverage.LineResults[29].Should().BeEquivalentTo(new { LineNumber = 29, Hits = 0, Branch = false });
            calculatorCoverage.LineResults[32].Should().BeEquivalentTo(new { LineNumber = 32, Hits = 0, Branch = false });
            calculatorCoverage.LineResults[33].Should().BeEquivalentTo(new { LineNumber = 33, Hits = 0, Branch = false });
            calculatorCoverage.LineResults[34].Should().BeEquivalentTo(new { LineNumber = 34, Hits = 0, Branch = false });
            calculatorCoverage.LineResults[9].Should().BeEquivalentTo(new { LineNumber = 9, Hits = 1, Branch = false });
            calculatorCoverage.LineResults[10].Should().BeEquivalentTo(new { LineNumber = 10, Hits = 1, Branch = false });
            calculatorCoverage.LineResults[11].Should().BeEquivalentTo(new { LineNumber = 11, Hits = 1, Branch = false });
            calculatorCoverage.LineResults[12].Should().BeEquivalentTo(new { LineNumber = 12, Hits = 1, Branch = false });
        }

        private void AssertCalendarResults(ClassResult calendarCoverage)
        {
            /* Line coverage expected is 
               <lines>
                <line number="8" hits="0" branch="False" />
                <line number="9" hits="0" branch="False" />
                <line number="10" hits="0" branch="False" />
                <line number="13" hits="0" branch="False" />
                <line number="14" hits="0" branch="False" />
                <line number="15" hits="0" branch="False" />
                <line number="18" hits="1" branch="False" />
                <line number="19" hits="1" branch="False" />
                <line number="20" hits="1" branch="False" />
               </lines>
          */
            calendarCoverage.LineResults.Count.Should().Be(9);

            calendarCoverage.LineResults[8].Should().BeEquivalentTo(new { LineNumber = 8, Hits = 0, Branch = false });
            calendarCoverage.LineResults[9].Should().BeEquivalentTo(new { LineNumber = 9, Hits = 0, Branch = false });
            calendarCoverage.LineResults[10].Should().BeEquivalentTo(new { LineNumber = 10, Hits = 0, Branch = false });
            calendarCoverage.LineResults[13].Should().BeEquivalentTo(new { LineNumber = 13, Hits = 0, Branch = false });
            calendarCoverage.LineResults[14].Should().BeEquivalentTo(new { LineNumber = 14, Hits = 0, Branch = false });
            calendarCoverage.LineResults[15].Should().BeEquivalentTo(new { LineNumber = 15, Hits = 0, Branch = false });
            calendarCoverage.LineResults[18].Should().BeEquivalentTo(new { LineNumber = 18, Hits = 1, Branch = false });
            calendarCoverage.LineResults[19].Should().BeEquivalentTo(new { LineNumber = 19, Hits = 1, Branch = false });
            calendarCoverage.LineResults[20].Should().BeEquivalentTo(new { LineNumber = 20, Hits = 1, Branch = false });
        }
    }
}
