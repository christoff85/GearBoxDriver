using System;
using FluentAssertions;
using GearboxDriver.Domain.ValueObjects;
using Xunit;

namespace GearboxDriver.Domain.Tests.ValueObjects
{
    public class GearTests
    {
        [Fact]
        public void Upshift_ShouldReturnNextGear_WhenNotOnMaxDrive()
        {
            var currentGear = new Gear(3, 5);
            var expectedNextGear = new Gear(4, 5);

            var result = currentGear.Upshift();

            result.Should().Be(expectedNextGear);
        }

        [Fact]
        public void Upshift_ShouldReturnSameGear_WhenOnMaxDrive()
        {
            var currentGear = new Gear(5, 5);

            var result = currentGear.Upshift();

            result.Should().Be(currentGear);
        }

        [Fact]
        public void Downshift_ShouldReturnPreviousGear_WhenNotOnFirstDrive()
        {
            var currentGear = new Gear(2, 5);
            var expectedNextGear = new Gear(1, 5);

            var result = currentGear.Downshift();

            result.Should().Be(expectedNextGear);
        }

        [Fact]
        public void Upshift_ShouldReturnSameGear_WhenOnFirstDrive()
        {
            var currentGear = new Gear(1, 5);

            var result = currentGear.Downshift();

            result.Should().Be(currentGear);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void Upshift_ShouldNotChangeGear_WhenOnNeutralOrReverseGear(int reverseOrNeutralGear)
        {
            var currentGear = new Gear(reverseOrNeutralGear, 5);

            var result = currentGear.Upshift();

            result.Should().Be(currentGear);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void Downshift_ShouldNotChangeGear_WhenOnNeutralOrReverseGear(int reverseOrNeutralGear)
        {
            var currentGear = new Gear(reverseOrNeutralGear, 5);

            var result = currentGear.Downshift();

            result.Should().Be(currentGear);
        }

        [Fact]
        public void Ctor_ShouldThrowArgumentOutOfRangeException_WhenGearLesserThanReverse()
        {
            Action act = () => new Gear(-2, 5);

            act.Should().Throw<ArgumentOutOfRangeException>()
                .Which.ParamName.Should().Be("currentGear");
        }

        [Fact]
        public void Ctor_ShouldThrowArgumentOutOfRangeException_WhenGearGreaterThanMaxDrive()
        {
            Action act = () => new Gear(6, 5);

            act.Should().Throw<ArgumentOutOfRangeException>()
                .Which.ParamName.Should().Be("currentGear");
        }
    }
}
