using System;
using FluentAssertions;
using GearboxDriver.Domain.AggressiveModes;
using GearboxDriver.Domain.DriveModes;
using GearboxDriver.Domain.ValueObjects;
using NSubstitute;
using Xunit;

namespace GearboxDriver.Domain.Tests.AggressiveModes
{
    public class AggressiveMode1Tests
    {
        private readonly IGearShifter _gearShifter = Substitute.For<IGearShifter>();
        private readonly IExternalSystems _externalSystems = Substitute.For<IExternalSystems>();

        [Fact]
        public void Accelerate_ShouldDownshift_WhenCurrentRpmIsLesserThanDownshiftBoundary()
        {
            var currentRpm = new Rpm(1000d);
            _externalSystems.GetCurrentRpm().Returns(currentRpm);

            var shiftBoundaries = GetShiftBoundaries(1500d, 3000d);

            Sut().Accelerate(shiftBoundaries);

            _gearShifter.Received(1).Downshift();
            _gearShifter.DidNotReceive().Upshift();
        }

        [Fact]
        public void Accelerate_ShouldUpshift_WhenCurrentRpmIsGreaterThanUpshiftBoundary()
        {
            var currentRpm = new Rpm(4000d);
            _externalSystems.GetCurrentRpm().Returns(currentRpm);

            var shiftBoundaries = GetShiftBoundaries(1500d, 3000d);

            Sut().Accelerate(shiftBoundaries);

            _gearShifter.Received(1).Upshift();
            _gearShifter.DidNotReceive().Downshift();
        }

        [Fact]
        public void Accelerate_ShouldNotUpshiftAndNotDownShift_WhenCurrentRpmIsBetweenShiftBoundaries()
        {
            var currentRpm = new Rpm(2000d);
            _externalSystems.GetCurrentRpm().Returns(currentRpm);

            var shiftBoundaries = GetShiftBoundaries(1500d, 3000d);

            Sut().Accelerate(shiftBoundaries);

            _gearShifter.DidNotReceive().Upshift();
            _gearShifter.DidNotReceive().Downshift();
        }

        [Fact]
        public void Ctor_ShouldThrowArgumentNullException_WhenGearShifterIsNull()
        {
            Action act = () => new AggressiveMode1(null, _externalSystems);

            act.Should().Throw<ArgumentNullException>()
                .Which.ParamName.Should().Be("gearShifter");
        }

        [Fact]
        public void Ctor_ShouldThrowArgumentNullException_WhenExternalSystemsIsNull()
        {
            Action act = () => new AggressiveMode1(_gearShifter, null);

            act.Should().Throw<ArgumentNullException>()
                .Which.ParamName.Should().Be("externalSystems");
        }

        private GearShiftBoundaries GetShiftBoundaries(double downshiftBoundary, double upshiftBoundary)
            => new GearShiftBoundaries(new Rpm(downshiftBoundary), new Rpm(upshiftBoundary));

        private AggressiveMode1 Sut() => new AggressiveMode1(_gearShifter, _externalSystems);
    }
}
