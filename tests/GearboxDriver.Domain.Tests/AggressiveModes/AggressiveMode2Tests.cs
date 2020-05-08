using System;
using FluentAssertions;
using GearboxDriver.Domain.AggressiveModes;
using GearboxDriver.Domain.DriveModes;
using GearboxDriver.Domain.ValueObjects;
using NSubstitute;
using Xunit;

namespace GearboxDriver.Domain.Tests.AggressiveModes
{
    public class AggressiveMode2Tests
    {
        private readonly double _rpmUpShiftFactorValue = 1.2d;
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
        public void Accelerate_ShouldUpshift_WhenCurrentRpmIsGreaterThanUpshiftBoundaryModifiedByFactor()
        {
            var currentRpm = new Rpm(3000d * _rpmUpShiftFactorValue + 1);
            _externalSystems.GetCurrentRpm().Returns(currentRpm);

            var shiftBoundaries = GetShiftBoundaries(1500d, 3000d);

            Sut().Accelerate(shiftBoundaries);

            _gearShifter.Received(1).Upshift();
            _gearShifter.DidNotReceive().Downshift();
        }

        [Fact]
        public void Accelerate_ShouldNotUpshift_WhenCurrentRpmIsGreaterThanUpshiftBoundaryNotModifiedByFactor()
        {
            var currentRpm = new Rpm(3000d + 1);
            _externalSystems.GetCurrentRpm().Returns(currentRpm);

            var shiftBoundaries = GetShiftBoundaries(1500d, 3000d);

            Sut().Accelerate(shiftBoundaries);

            _gearShifter.DidNotReceive().Upshift();
            _gearShifter.DidNotReceive().Downshift();
        }

        [Fact]
        public void Ctor_ShouldThrowArgumentNullException_WhenRpmUpshiftFactorIsNull()
        {
            Action act = () => new AggressiveMode2(null, _gearShifter, _externalSystems);

            act.Should().Throw<ArgumentNullException>()
                .Which.ParamName.Should().Be("rpmUpshiftFactor");
        }

        [Fact]
        public void Ctor_ShouldThrowArgumentNullException_WhenGearShifterIsNull()
        {
            Action act = () => new AggressiveMode2(new RpmShiftFactor(_rpmUpShiftFactorValue), null, _externalSystems);

            act.Should().Throw<ArgumentNullException>()
                .Which.ParamName.Should().Be("gearShifter");
        }

        [Fact]
        public void Ctor_ShouldThrowArgumentNullException_WhenExternalSystemsIsNull()
        {
            Action act = () => new AggressiveMode2(new RpmShiftFactor(_rpmUpShiftFactorValue), _gearShifter, null);

            act.Should().Throw<ArgumentNullException>()
                .Which.ParamName.Should().Be("externalSystems");
        }

        private GearShiftBoundaries GetShiftBoundaries(double downshiftBoundary, double upshiftBoundary) 
            => new GearShiftBoundaries(new Rpm(downshiftBoundary), new Rpm(upshiftBoundary));

        private AggressiveMode2 Sut() => new AggressiveMode2(new RpmShiftFactor(_rpmUpShiftFactorValue), _gearShifter, _externalSystems);
    }
}
