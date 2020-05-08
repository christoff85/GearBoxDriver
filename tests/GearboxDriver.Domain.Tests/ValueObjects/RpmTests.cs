using System;
using FluentAssertions;
using GearboxDriver.Domain.ValueObjects;
using Xunit;

namespace GearboxDriver.Domain.Tests.ValueObjects
{
    public class RpmTests
    {
        [Fact]
        public void Ctor_ShouldThrowArgumentOutOfRangeException_WhenRpmShiftFactorLesserThanZero()
        {
            Action act = () => new Rpm(-0.1d);

            act.Should().Throw<ArgumentOutOfRangeException>()
                .Which.ParamName.Should().Be("rpm");
        }

        [Fact]
        public void LesserThan_ShouldReturnTrue_WhenComparingLesserValueWithGreaterValue()
        {
            var lesser = new Rpm(1000d);
            var greater = new Rpm(2000d);

            var result = lesser < greater;

            result.Should().BeTrue();
        }

        [Fact]
        public void LesserThan_ShouldReturnFalse_WhenComparingGreaterValueWithLesserValue()
        {
            var lesser = new Rpm(1000d);
            var greater = new Rpm(2000d);

            var result = greater < lesser;

            result.Should().BeFalse();
        }

        [Fact]
        public void GreaterThan_ShouldReturnTrue_WhenComparingGreaterValueWithLesserValue()
        {
            var lesser = new Rpm(1000d);
            var greater = new Rpm(2000d);

            var result = greater > lesser;

            result.Should().BeTrue();
        }

        [Fact]
        public void GreaterThan_ShouldReturnFalse_WhenComparingLesserValueWithGreaterValue()
        {
            var lesser = new Rpm(1000d);
            var greater = new Rpm(2000d);

            var result = lesser > greater;

            result.Should().BeFalse();
        }
    }
}