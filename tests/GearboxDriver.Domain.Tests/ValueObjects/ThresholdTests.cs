using System;
using FluentAssertions;
using GearboxDriver.Domain.ValueObjects;
using Xunit;

namespace GearboxDriver.Domain.Tests.ValueObjects
{
    public class ThresholdTests
    {
        [Fact]
        public void IsKickdown_ShouldReturnFalse_WhenThresholdLesserThanKickdownBoundary()
        {
            var threshold = new Threshold(0.4d);
            
            var result = threshold.IsKickdown();

            result.Should().BeFalse();
        }

        [Fact]
        public void IsKickdown_ShouldReturnTrue_WhenThresholdGreaterThanKickdownBoundary()
        {
            var threshold = new Threshold(0.6d);
            
            var result = threshold.IsKickdown();

            result.Should().BeTrue();
        }

        [Fact]
        public void IsStrongKickdown_ShouldReturnFalse_WhenThresholdLesserThanStrongKickdownBoundary()
        {
            var threshold = new Threshold(0.6d);
            
            var result = threshold.IsStrongKickDown();

            result.Should().BeFalse();
        }

        [Fact]
        public void IsStrongKickdown_ShouldReturnTrue_WhenThresholdGreaterThanStrongKickdownBoundary()
        {
            var threshold = new Threshold(0.8d);
            
            var result = threshold.IsStrongKickDown();

            result.Should().BeTrue();
        }

        [Fact]
        public void Ctor_ShouldThrowArgumentOutOfRangeException_WhenThresholdLesserThanZero()
        {
            Action act = () => new Threshold(-0.1d);

            act.Should().Throw<ArgumentOutOfRangeException>()
                .Which.ParamName.Should().Be("threshold");
        }
    }
}