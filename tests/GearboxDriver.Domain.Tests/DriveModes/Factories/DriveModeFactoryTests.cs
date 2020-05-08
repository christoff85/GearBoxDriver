using System;
using FluentAssertions;
using GearboxDriver.Domain.AggressiveModes.Factories;
using GearboxDriver.Domain.DriveModes;
using GearboxDriver.Domain.DriveModes.Factories;
using GearboxDriver.Domain.DriveModes.Providers;
using GearboxDriver.Domain.ValueObjects;
using NSubstitute;
using Xunit;

namespace GearboxDriver.Domain.Tests.DriveModes.Factories
{
    public class DriveModeFactoryTests
    {
        private readonly IAggressiveModeFactory _aggressiveModeFactory = Substitute.For<IAggressiveModeFactory>();
        private readonly IDriveModeParametersProvider _driveModeParametersProvider = Substitute.For<IDriveModeParametersProvider>();
        private readonly IGearShifter _gearShifter = Substitute.For<IGearShifter>();
        private readonly IExternalSystems _externalSystems = Substitute.For<IExternalSystems>();
        
        [Fact]
        public void Create_ShouldCreateInstanceOfEcoMode_WhenEcoModeValueIsProvided()
        {
            _driveModeParametersProvider.DriveModeValue.Returns(DriveModeValue.EcoMode);
            _driveModeParametersProvider.EcoModeGearShiftBoundaries.Returns(new GearShiftBoundaries(new Rpm(1000d), new Rpm(2000d)));


            var driveMode = Sut().Create();

            driveMode.Should().BeOfType<EcoMode>();
        }

        [Fact]
        public void Create_ShouldCreateInstanceOfComfortMode_WhenComfortModeValueIsProvided()
        {
            _driveModeParametersProvider.DriveModeValue.Returns(DriveModeValue.ComfortMode);
            _driveModeParametersProvider.ComfortModeGearShiftBoundaries.Returns(new GearShiftBoundaries(new Rpm(1000d), new Rpm(2000d)));

            var driveMode = Sut().Create();

            driveMode.Should().BeOfType<ComfortMode>();
        }

        [Fact]
        public void Create_ShouldCreateInstanceOfSportMode_WhenSportModeValueIsProvided()
        {
            _driveModeParametersProvider.DriveModeValue.Returns(DriveModeValue.SportMode);
            _driveModeParametersProvider.SportModeGearShiftBoundaries.Returns(new GearShiftBoundaries(new Rpm(1000d), new Rpm(2000d)));


            var driveMode = Sut().Create();

            driveMode.Should().BeOfType<SportMode>();
        }

        [Fact]
        public void Ctor_ShouldThrowArgumentNullException_WhenAggressiveModeFactoryIsNull()
        {
            Action act = () => new DriveModeFactory(null, _driveModeParametersProvider, _gearShifter, _externalSystems);

            act.Should().Throw<ArgumentNullException>()
                .Which.ParamName.Should().Be("aggressiveModeFactory");
        }

        [Fact]
        public void Ctor_ShouldThrowArgumentNullException_WhenDriveModeParametersProviderIsNull()
        {
            Action act = () => new DriveModeFactory(_aggressiveModeFactory, null, _gearShifter, _externalSystems);

            act.Should().Throw<ArgumentNullException>()
                .Which.ParamName.Should().Be("driveModeParametersProvider");
        }

        [Fact]
        public void Ctor_ShouldThrowArgumentNullException_WhenGearShifterIsNull()
        {
            Action act = () => new DriveModeFactory(_aggressiveModeFactory, _driveModeParametersProvider, null, _externalSystems);

            act.Should().Throw<ArgumentNullException>()
                .Which.ParamName.Should().Be("gearShifter");
        }

        [Fact]
        public void Ctor_ShouldThrowArgumentNullException_WhenExternalSystemsIsNull()
        {
            Action act = () => new DriveModeFactory(_aggressiveModeFactory, _driveModeParametersProvider, _gearShifter, null);

            act.Should().Throw<ArgumentNullException>()
                .Which.ParamName.Should().Be("externalSystems");
        }

        private DriveModeFactory Sut() => new DriveModeFactory(_aggressiveModeFactory, _driveModeParametersProvider, _gearShifter, _externalSystems);
    }
}