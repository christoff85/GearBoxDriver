using System;
using FluentAssertions;
using GearboxDriver.Domain.DriveModes;
using GearboxDriver.Domain.ValueObjects;
using NSubstitute;
using Xunit;

namespace GearboxDriver.Domain.Tests.DriveModes
{
    public class TrailerModeTests
    {
        private readonly IDriveMode _baseMode = Substitute.For<IDriveMode>();
        private readonly IGearShifter _gearShifter = Substitute.For<IGearShifter>();
        private readonly IExternalSystems _externalSystems = Substitute.For<IExternalSystems>();

        [Fact]
        public void Accelerate_ShouldDownshiftAndCallAccelerateOnBaseMode_WhenDrivingDown()
        {
            _externalSystems.IsDrivingDown().Returns(true);

            var threshold = new Threshold(0.8d);
            Sut().Accelerate(threshold);

            _gearShifter.Received().Downshift();
            _baseMode.Received().Accelerate(threshold);
        }

        [Fact]
        public void Accelerate_ShouldOnlyCallAccelerateOnBaseMode_WhenNotDrivingDown()
        {
            _externalSystems.IsDrivingDown().Returns(false);

            var threshold = new Threshold(0.8d);
            Sut().Accelerate(threshold);

            _gearShifter.DidNotReceive().Downshift();
            _baseMode.Received().Accelerate(threshold);
        }

        [Fact]
        public void Ctor_ShouldThrowArgumentNullException_WhenBaseModeIsNull()
        {
            Action act = () => new TrailerMode(null, _gearShifter, _externalSystems);

            act.Should().Throw<ArgumentNullException>()
                .Which.ParamName.Should().Be("baseMode");
        }

        [Fact]
        public void Ctor_ShouldThrowArgumentNullException_WhenGearShifterIsNull()
        {
            Action act = () => new TrailerMode(_baseMode, null, _externalSystems);

            act.Should().Throw<ArgumentNullException>()
                .Which.ParamName.Should().Be("gearShifter");
        }

        [Fact]
        public void Ctor_ShouldThrowArgumentNullException_WhenExternalSystemsIsNull()
        {
            Action act = () => new TrailerMode(_baseMode, _gearShifter, null);

            act.Should().Throw<ArgumentNullException>()
                .Which.ParamName.Should().Be("externalSystems");
        }

        private TrailerMode Sut() => new TrailerMode(_baseMode, _gearShifter, _externalSystems);
    }
}