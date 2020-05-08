using System;
using FluentAssertions;
using GearboxDriver.Domain.ValueObjects;
using Xunit;

namespace GearboxDriver.Domain.Tests.ValueObjects
{
    public class RpmShiftFactorTests
    {
        [Fact]
        public void Ctor_ShouldThrowArgumentOutOfRangeException_WhenRpmShiftFactorLesserThanZero()
        {
            Action act = () => new RpmShiftFactor(-0.1d);

            act.Should().Throw<ArgumentOutOfRangeException>()
                .Which.ParamName.Should().Be("rpmShiftFactor");
        }
    }
}