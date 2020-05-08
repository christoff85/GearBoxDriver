using System;
using FluentAssertions;
using GearboxDriver.Domain.AggressiveModes;
using GearboxDriver.Domain.AggressiveModes.Factories;
using GearboxDriver.Domain.AggressiveModes.Providers;
using GearboxDriver.Domain.ValueObjects;
using NSubstitute;
using Xunit;

namespace GearboxDriver.Domain.Tests.AggressiveModes.Factories
{
    public class AggressiveModeFactoryTests
    {
        private readonly IAggressiveModeParametersProvider _aggressiveModeParametersProvider = Substitute.For<IAggressiveModeParametersProvider>();
        private readonly IGearShifter _gearShifter = Substitute.For<IGearShifter>();
        private readonly IExternalSystems _externalSystems = Substitute.For<IExternalSystems>();

        [Fact]
        public void Create_ShouldCreateInstanceOfAggressiveMode1_WhenAggressiveMode1ValueIsProvided()
        {
            _aggressiveModeParametersProvider.AggressiveModeValue.Returns(AggressiveModeValue.AggresiveMode1);

            var aggressiveMode = Sut().Create();

            aggressiveMode.Should().BeOfType<AggressiveMode1>();
        }

        [Fact]
        public void Create_ShouldCreateInstanceOfAggressiveMode2_WhenAggressiveMode2ValueIsProvided()
        {
            _aggressiveModeParametersProvider.AggressiveModeValue.Returns(AggressiveModeValue.AggresiveMode2);
            _aggressiveModeParametersProvider.AggressiveMode2UpshiftFactor.Returns(new RpmShiftFactor(1.2d));

            var aggressiveMode = Sut().Create();

            aggressiveMode.Should().BeOfType<AggressiveMode2>();
        }

        [Fact]
        public void Create_ShouldCreateInstanceOfAggressiveMode3_WhenAggressiveMode3ValueIsProvided()
        {
            _aggressiveModeParametersProvider.AggressiveModeValue.Returns(AggressiveModeValue.AggresiveMode3);
            _aggressiveModeParametersProvider.AggressiveMode3UpshiftFactor.Returns(new RpmShiftFactor(1.3d));


            var aggressiveMode = Sut().Create();

            aggressiveMode.Should().BeOfType<AggressiveMode3>();
        }
        
        [Fact]
        public void Ctor_ShouldThrowArgumentNullException_WhenAggressiveModeValueProviderIsNull()
        {
            Action act = () => new AggressiveModeFactory(null, _gearShifter, _externalSystems);

            act.Should().Throw<ArgumentNullException>()
                .Which.ParamName.Should().Be("aggressiveModeParametersProvider");
        }
        [Fact]

        public void Ctor_ShouldThrowArgumentNullException_WhenGearShifterIsNull()
        {
            Action act = () => new AggressiveModeFactory(_aggressiveModeParametersProvider, null, _externalSystems);

            act.Should().Throw<ArgumentNullException>()
                .Which.ParamName.Should().Be("gearShifter");
        }

        [Fact]
        public void Ctor_ShouldThrowArgumentNullException_WhenExternalSystemsIsNull()
        {
            Action act = () => new AggressiveModeFactory(_aggressiveModeParametersProvider, _gearShifter, null);

            act.Should().Throw<ArgumentNullException>()
                .Which.ParamName.Should().Be("externalSystems");
        }

        private AggressiveModeFactory Sut() => new AggressiveModeFactory(_aggressiveModeParametersProvider, _gearShifter, _externalSystems);
    }
}