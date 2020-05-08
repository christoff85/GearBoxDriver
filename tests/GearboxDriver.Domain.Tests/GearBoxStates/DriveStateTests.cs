using System;
using FluentAssertions;
using GearboxDriver.Domain.DriveModes;
using GearboxDriver.Domain.DriveModes.Factories;
using GearboxDriver.Domain.GearBoxStates;
using GearboxDriver.Domain.ValueObjects;
using NSubstitute;
using Xunit;

namespace GearboxDriver.Domain.Tests.GearBoxStates
{
    public class DriveStateTests
    {
        private readonly IDriveModeFactory _driveModeFactory = Substitute.For<IDriveModeFactory>();
        private readonly IGearShifter _gearShifter = Substitute.For<IGearShifter>();

        [Fact]
        public void Accelerate_ShouldCallAccelerateOnCurrentDriveMode()
        {
            var currentDriveMode = Substitute.For<IDriveMode>();
            _driveModeFactory.Create().Returns(currentDriveMode);

            var threshold = new Threshold(0.8d);
            Sut().Accelerate(threshold);

            currentDriveMode.Received().Accelerate(threshold);
        }
        
        [Fact]
        public void ManualUpshift_ShouldUpshiftGear()
        {
            Sut().ManualUpshift();
            _gearShifter.Received().Upshift();
        }

        [Fact]
        public void ManualDownshift_ShouldDownshiftGear()
        {
            Sut().ManualDownshift();
            _gearShifter.Received().Downshift();
        }
        
        [Fact]
        public void Ctor_ShouldThrowArgumentNullException_WhenDriveModeFactoryIsNull()
        {
            Action act = () => new DriveState(null, _gearShifter);

            act.Should().Throw<ArgumentNullException>()
                .Which.ParamName.Should().Be("driveModeFactory");
        }

        [Fact]
        public void Ctor_ShouldThrowArgumentNullException_WhenGearShifterIsNull()
        {
            Action act = () => new DriveState(_driveModeFactory, null);

            act.Should().Throw<ArgumentNullException>()
                .Which.ParamName.Should().Be("gearShifter");
        }

        private DriveState Sut() => new DriveState(_driveModeFactory, _gearShifter);

    }
}