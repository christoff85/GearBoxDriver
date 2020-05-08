using System;
using FluentAssertions;
using GearboxDriver.Domain.ValueObjects;
using NSubstitute;
using Xunit;

namespace GearboxDriver.Domain.Tests
{
    public class GearShifterTests
    {
        private readonly IGearBox _gearBox = Substitute.For<IGearBox>();
        
        [Fact]
        public void Upshift_ShouldSetSecondGear_WhenReceivedFirst()
        {
            var first = new Gear(1, 5);
            var second = new Gear(2, 5);

            _gearBox.GetCurrentGear().Returns(first);

            Sut().Upshift();

            _gearBox.Received().SetCurrentGear(second);
        }

        [Fact]
        public void Downshift_ShouldSetFirstGear_WhenReceivedSecond()
        {
            var first = new Gear(1, 5);
            var second = new Gear(2, 5);

            _gearBox.GetCurrentGear().Returns(second);

            Sut().Downshift();

            _gearBox.Received().SetCurrentGear(first);
        }

        [Fact]
        public void Ctor_ShouldThrowArgumentNullException_WhenGearBoxIsNull()
        {
            Action act = () => new GearShifter(null);

            act.Should().Throw<ArgumentNullException>()
                .Which.ParamName.Should().Be("gearBox");
        }

        private GearShifter Sut() => new GearShifter(_gearBox);
    }
}
