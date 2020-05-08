using System;
using FluentAssertions;
using GearboxDriver.Domain.AggressiveModes;
using GearboxDriver.Domain.DriveModes;
using GearboxDriver.Domain.ValueObjects;
using NSubstitute;
using Xunit;

namespace GearboxDriver.Domain.Tests.AggressiveModes
{
    public class AggressiveMode3Tests
    {
        private readonly double _rpmUpShiftFactorValue = 1.3d;
        private readonly IGearShifter _gearShifter = Substitute.For<IGearShifter>();
        private readonly IExternalSystems _externalSystems = Substitute.For<IExternalSystems>();

        [Fact]
        public void Accelerate_ShouldDownshiftAndNotMakeSound_WhenCurrentRpmIsLesserThanDownshiftBoundary()
        {
            var currentRpm = new Rpm(1000d);
            _externalSystems.GetCurrentRpm().Returns(currentRpm);

            var shiftBoundaries = GetShiftBoundaries(1500d, 3000d);

            Sut().Accelerate(shiftBoundaries);

            _gearShifter.Received().Downshift();
            _gearShifter.DidNotReceive().Upshift();
            _externalSystems.DidNotReceive().MakeSound(Arg.Any<SoundVolume>());
        }

        [Fact]
        public void Accelerate_ShouldUpshiftAndMakeSound_WhenCurrentRpmIsGreaterThanUpshiftBoundaryModifiedByFactor()
        {
            var currentRpm = new Rpm(3000d * _rpmUpShiftFactorValue + 1);
            _externalSystems.GetCurrentRpm().Returns(currentRpm);

            var shiftBoundaries = GetShiftBoundaries(1500d, 3000d);

            Sut().Accelerate(shiftBoundaries);

            _gearShifter.Received().Upshift();
            _gearShifter.DidNotReceive().Downshift();

            _externalSystems.Received().MakeSound(Arg.Any<SoundVolume>());
        }

        [Fact]
        public void Accelerate_ShouldNotUpshiftAndNotMakeSound_WhenCurrentRpmIsGreaterThanUpshiftBoundaryNotModifiedByFactor()
        {
            var currentRpm = new Rpm(3000d + 1);
            _externalSystems.GetCurrentRpm().Returns(currentRpm);

            var shiftBoundaries = GetShiftBoundaries(1500d, 3000d);

            Sut().Accelerate(shiftBoundaries);

            _gearShifter.DidNotReceive().Upshift();
            _gearShifter.DidNotReceive().Downshift();
            _externalSystems.DidNotReceive().MakeSound(Arg.Any<SoundVolume>());
        }

        [Fact]
        public void Ctor_ShouldThrowArgumentNullException_WhenRpmUpshiftFactorIsNull()
        {
            Action act = () => new AggressiveMode3(null, _gearShifter, _externalSystems);

            act.Should().Throw<ArgumentNullException>()
                .Which.ParamName.Should().Be("rpmUpshiftFactor");
        }

        [Fact]
        public void Ctor_ShouldThrowArgumentNullException_WhenGearShifterIsNull()
        {
            Action act = () => new AggressiveMode3(new RpmShiftFactor(_rpmUpShiftFactorValue), null, _externalSystems);

            act.Should().Throw<ArgumentNullException>()
                .Which.ParamName.Should().Be("gearShifter");
        }

        [Fact]
        public void Ctor_ShouldThrowArgumentNullException_WhenExternalSystemsIsNull()
        {
            Action act = () => new AggressiveMode3(new RpmShiftFactor(_rpmUpShiftFactorValue), _gearShifter, null);

            act.Should().Throw<ArgumentNullException>()
                .Which.ParamName.Should().Be("externalSystems");
        }

        private GearShiftBoundaries GetShiftBoundaries(double downshiftBoundary, double upshiftBoundary)
            => new GearShiftBoundaries(new Rpm(downshiftBoundary), new Rpm(upshiftBoundary));

        private AggressiveMode3 Sut() => new AggressiveMode3(new RpmShiftFactor(_rpmUpShiftFactorValue), _gearShifter, _externalSystems);
    }
}
