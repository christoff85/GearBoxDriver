using System;
using FluentAssertions;
using GearboxDriver.Domain.DriveModes.Factories;
using GearboxDriver.Domain.GearBoxStates;
using GearboxDriver.Domain.GearBoxStates.Factories;
using GearboxDriver.Domain.GearBoxStates.Providers;
using NSubstitute;
using Xunit;

namespace GearboxDriver.Domain.Tests.GearBoxStates.Factories
{
    public class GearBoxStateFactoryTests
    {
        private readonly IDriveModeFactory _driveModeFactory = Substitute.For<IDriveModeFactory>();
        private readonly IGearBoxStateValueProvider _gearBoxStateValueProvider = Substitute.For<IGearBoxStateValueProvider>();
        private readonly IGearShifter _gearShifter = Substitute.For<IGearShifter>();

        [Fact]
        public void Create_ShouldCreateInstanceOfDriveState_WhenDriveStateValueIsProvided()
        {
            _gearBoxStateValueProvider.GearBoxStateValue.Returns(GearBoxStateValue.DriveState);

            var aggressiveMode = Sut().Create();

            aggressiveMode.Should().BeOfType<DriveState>();
        }

        [Fact]
        public void Create_ShouldCreateInstanceOfParkState_WhenParkStateValueIsProvided()
        {
            _gearBoxStateValueProvider.GearBoxStateValue.Returns(GearBoxStateValue.ParkState);

            var aggressiveMode = Sut().Create();

            aggressiveMode.Should().BeOfType<ParkState>();
        }

        [Fact]
        public void Create_ShouldCreateInstanceOfReverseState_WhenReverseStateValueIsProvided()
        {
            _gearBoxStateValueProvider.GearBoxStateValue.Returns(GearBoxStateValue.ReverseState);

            var aggressiveMode = Sut().Create();

            aggressiveMode.Should().BeOfType<ReverseState>();
        }

        [Fact]
        public void Create_ShouldCreateInstanceOfNeutralState_WhenNeutralStateValueIsProvided()
        {
            _gearBoxStateValueProvider.GearBoxStateValue.Returns(GearBoxStateValue.NeutralState);

            var aggressiveMode = Sut().Create();

            aggressiveMode.Should().BeOfType<NeutralState>();
        }
        
        [Fact]
        public void Ctor_ShouldThrowArgumentNullException_WhenDriveModeFactoryIsNull()
        {
            Action act = () => new GearBoxStateFactory(null, _gearBoxStateValueProvider, _gearShifter);

            act.Should().Throw<ArgumentNullException>()
                .Which.ParamName.Should().Be("driveModeFactory");
        }

        [Fact]
        public void Ctor_ShouldThrowArgumentNullException_WhenGearBoxStateValueProviderIsNull()
        {
            Action act = () => new GearBoxStateFactory(_driveModeFactory, null, _gearShifter);

            act.Should().Throw<ArgumentNullException>()
                .Which.ParamName.Should().Be("gearBoxStateValueProvider");
        }

        [Fact]
        public void Ctor_ShouldThrowArgumentNullException_WhenGearShifterIsNull()
        {
            Action act = () => new GearBoxStateFactory(_driveModeFactory, _gearBoxStateValueProvider, null);

            act.Should().Throw<ArgumentNullException>()
                .Which.ParamName.Should().Be("gearShifter");
        }

        private GearBoxStateFactory Sut() => new GearBoxStateFactory(_driveModeFactory, _gearBoxStateValueProvider, _gearShifter);
    }
}