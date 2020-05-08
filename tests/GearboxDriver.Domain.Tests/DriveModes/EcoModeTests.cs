using System;
using FluentAssertions;
using GearboxDriver.Domain.DriveModes;
using GearboxDriver.Domain.ValueObjects;
using NSubstitute;
using Xunit;

namespace GearboxDriver.Domain.Tests.DriveModes
{
    public class EcoModeTests
    {
        private readonly GearShiftBoundaries _gearShiftBoundaries = new GearShiftBoundaries(new Rpm(1000d), new Rpm(2000d));
        private readonly IGearShifter _gearShifter = Substitute.For<IGearShifter>();
        private readonly IExternalSystems _externalSystems = Substitute.For<IExternalSystems>();

        [Fact]
        public void Accelerate_ShouldDownshift_WhenCurrentRpmIsLesserThanDownshiftBoundary()
        {
            var currentRpm = new Rpm(800d);
            _externalSystems.GetCurrentRpm().Returns(currentRpm);

            var threshold = new Threshold(0.8d);
            Sut().Accelerate(threshold);

            _gearShifter.Received(1).Downshift();
            _gearShifter.DidNotReceive().Upshift();
        }

        [Fact]
        public void Accelerate_ShouldUpshift_WhenCurrentRpmIsGreaterThanUpshiftBoundary()
        {
            var currentRpm = new Rpm(3000d);
            _externalSystems.GetCurrentRpm().Returns(currentRpm);

            var threshold = new Threshold(0.8d);
            Sut().Accelerate(threshold);

            _gearShifter.Received(1).Upshift();
            _gearShifter.DidNotReceive().Downshift();
        }

        [Fact]
        public void Accelerate_ShouldNotUpshiftAndNotDownShift_WhenCurrentRpmIsBetweenShiftBoundaries()
        {
            var currentRpm = new Rpm(1500d);
            _externalSystems.GetCurrentRpm().Returns(currentRpm);

            var threshold = new Threshold(0.8d);
            Sut().Accelerate(threshold);

            _gearShifter.DidNotReceive().Upshift();
            _gearShifter.DidNotReceive().Downshift();
        }

        [Fact]
        public void Ctor_ShouldThrowArgumentNullException_WhenGearShiftBoundariesIsNull()
        {
            Action act = () => new EcoMode(null, _gearShifter, _externalSystems);

            act.Should().Throw<ArgumentNullException>()
                .Which.ParamName.Should().Be("gearShiftBoundaries");
        }

        [Fact]
        public void Ctor_ShouldThrowArgumentNullException_WhenGearShifterIsNull()
        {
            Action act = () => new EcoMode(_gearShiftBoundaries, null, _externalSystems);

            act.Should().Throw<ArgumentNullException>()
                .Which.ParamName.Should().Be("gearShifter");
        }

        [Fact]
        public void Ctor_ShouldThrowArgumentNullException_WhenExternalSystemsIsNull()
        {
            Action act = () => new EcoMode(_gearShiftBoundaries, _gearShifter, null);

            act.Should().Throw<ArgumentNullException>()
                .Which.ParamName.Should().Be("externalSystems");
        }

        private EcoMode Sut() => new EcoMode(_gearShiftBoundaries, _gearShifter, _externalSystems);
    }
}