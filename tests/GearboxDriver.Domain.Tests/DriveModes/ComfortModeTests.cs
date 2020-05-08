using System;
using FluentAssertions;
using GearboxDriver.Domain.AggressiveModes;
using GearboxDriver.Domain.AggressiveModes.Factories;
using GearboxDriver.Domain.DriveModes;
using GearboxDriver.Domain.ValueObjects;
using NSubstitute;
using Xunit;

namespace GearboxDriver.Domain.Tests.DriveModes
{
    public class ComfortModeTests
    {
        private readonly GearShiftBoundaries _gearShiftBoundaries = new GearShiftBoundaries(new Rpm(1000d), new Rpm(4500d));
        private readonly IGearShifter _gearShifter = Substitute.For<IGearShifter>();
        private readonly IAggressiveModeFactory _aggressiveModeFactory = Substitute.For<IAggressiveModeFactory>();

        [Fact]
        public void Accelerate_ShouldDownshiftOnce_WhenKickdown()
        {
            var threshold = new Threshold(0.6d);

            Sut().Accelerate(threshold);

            _gearShifter.Received(1).Downshift();
        }

        [Fact]
        public void Accelerate_ShouldDownshiftOnce_WhenStrongKickdown()
        {
            var threshold = new Threshold(0.8d);

            Sut().Accelerate(threshold);

            _gearShifter.Received(1).Downshift();
        }

        [Fact]
        public void Accelerate_ShouldCallAccelerateOnCurrectAggressiveMode_WhenNotKickdown()
        {
            var aggressiveMode = Substitute.For<IAggressiveMode>();
            _aggressiveModeFactory.Create().Returns(aggressiveMode);

            var threshold = new Threshold(0.4d);

            Sut().Accelerate(threshold);

            _gearShifter.DidNotReceive().Downshift();
            _gearShifter.DidNotReceive().Upshift();
            aggressiveMode.Received().Accelerate(_gearShiftBoundaries);
        }

        [Fact]
        public void Ctor_ShouldThrowArgumentNullException_WhenGearShiftBoundariesIsNull()
        {
            Action act = () => new ComfortMode(null, _gearShifter, _aggressiveModeFactory);

            act.Should().Throw<ArgumentNullException>()
                .Which.ParamName.Should().Be("gearShiftBoundaries");
        }

        [Fact]
        public void Ctor_ShouldThrowArgumentNullException_WhenGearShifterIsNull()
        {
            Action act = () => new ComfortMode(_gearShiftBoundaries, null, _aggressiveModeFactory);

            act.Should().Throw<ArgumentNullException>()
                .Which.ParamName.Should().Be("gearShifter");
        }

        [Fact]
        public void Ctor_ShouldThrowArgumentNullException_WhenAggressiveModeFactoryIsNull()
        {
            Action act = () => new ComfortMode(_gearShiftBoundaries, _gearShifter, null);

            act.Should().Throw<ArgumentNullException>()
                .Which.ParamName.Should().Be("aggressiveModeFactory");
        }

        private ComfortMode Sut() => new ComfortMode(_gearShiftBoundaries, _gearShifter, _aggressiveModeFactory);
    }
}