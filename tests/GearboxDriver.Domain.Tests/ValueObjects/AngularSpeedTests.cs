using System;
using FluentAssertions;
using GearboxDriver.Domain.ValueObjects;
using Xunit;

namespace GearboxDriver.Domain.Tests.ValueObjects
{
    public class AngularSpeedTests
    {
        [Fact]
        public void Ctor_ShouldThrowArgumentOutOfRangeException_WhenRpmShiftFactorLesserThanZero()
        {
            Action act = () => new AngularSpeed(-0.1d);

            act.Should().Throw<ArgumentOutOfRangeException>()
                .Which.ParamName.Should().Be("angularSpeed");
        }

        [Fact]
        public void LesserThan_ShouldReturnTrue_WhenComparingLesserValueWithGreaterValue()
        {
            var lesser = new AngularSpeed(50d);
            var greater = new AngularSpeed(70d);

            var result = lesser < greater;

            result.Should().BeTrue();
        }

        [Fact]
        public void LesserThan_ShouldReturnFalse_WhenComparingGreaterValueWithLesserValue()
        {
            var lesser = new AngularSpeed(50d);
            var greater = new AngularSpeed(70d);

            var result = greater < lesser;

            result.Should().BeFalse();
        }

        [Fact]
        public void GreaterThan_ShouldReturnTrue_WhenComparingGreaterValueWithLesserValue()
        {
            var lesser = new AngularSpeed(50d);
            var greater = new AngularSpeed(70d);

            var result = greater > lesser;

            result.Should().BeTrue();
        }

        [Fact]
        public void GreaterThan_ShouldReturnFalse_WhenComparingLesserValueWithGreaterValue()
        {
            var lesser = new AngularSpeed(50d);
            var greater = new AngularSpeed(70d);

            var result = lesser > greater;

            result.Should().BeFalse();
        }
    }
}