using System;
using FluentAssertions;
using GearboxDriver.Domain.DriveModes;
using GearboxDriver.Domain.DriveModes.Factories;
using GearboxDriver.Domain.DriveModes.Providers;
using NSubstitute;
using Xunit;

namespace GearboxDriver.Domain.Tests.DriveModes.Factories
{
    public class TrailerModeFactoryTests
    {
        private readonly IDriveModeFactory _baseModeFactory = Substitute.For<IDriveModeFactory>();
        private readonly ITrailerModeParametersProvider _trailerModeParametersProvider = Substitute.For<ITrailerModeParametersProvider>();
        private readonly IGearShifter _gearShifter = Substitute.For<IGearShifter>();
        private readonly IExternalSystems _externalSystems = Substitute.For<IExternalSystems>();

        [Fact]
        public void Create_ShouldReturnBaseMode_WhenTrailerIsNotAttached()
        {
            var baseMode = Substitute.For<IDriveMode>();
            _baseModeFactory.Create().Returns(baseMode);
            _trailerModeParametersProvider.IsTrailerAttached.Returns(false);

            var result = Sut().Create();

            result.Should().Be(baseMode);
        }

        [Fact]
        public void Create_ShouldReturnInstanceOfTrailerMode_WhenTrailerIsAttached()
        {
            var baseMode = Substitute.For<IDriveMode>();
            _baseModeFactory.Create().Returns(baseMode);
            _trailerModeParametersProvider.IsTrailerAttached.Returns(true);

            var result = Sut().Create();

            result.Should().NotBe(baseMode);
            result.Should().BeOfType<TrailerMode>();
        }
        
        [Fact]
        public void Ctor_ShouldThrowArgumentNullException_WhenBaseModeFactoryIsNull()
        {
            Action act = () => new TrailerModeFactory(null, _trailerModeParametersProvider, _gearShifter, _externalSystems);

            act.Should().Throw<ArgumentNullException>()
                .Which.ParamName.Should().Be("baseModeFactory");
        }

        [Fact]
        public void Ctor_ShouldThrowArgumentNullException_WhenTrailerModeParametersProviderIsNull()
        {
            Action act = () => new TrailerModeFactory(_baseModeFactory, null, _gearShifter, _externalSystems);

            act.Should().Throw<ArgumentNullException>()
                .Which.ParamName.Should().Be("trailerModeParametersProvider");
        }

        [Fact]
        public void Ctor_ShouldThrowArgumentNullException_WhenGearShifterIsNull()
        {
            Action act = () => new TrailerModeFactory(_baseModeFactory, _trailerModeParametersProvider, null, _externalSystems);

            act.Should().Throw<ArgumentNullException>()
                .Which.ParamName.Should().Be("gearShifter");
        }

        [Fact]
        public void Ctor_ShouldThrowArgumentNullException_WhenExternalSystemsIsNull()
        {
            Action act = () => new TrailerModeFactory(_baseModeFactory, _trailerModeParametersProvider, _gearShifter, null);

            act.Should().Throw<ArgumentNullException>()
                .Which.ParamName.Should().Be("externalSystems");
        }

        private TrailerModeFactory Sut() => new TrailerModeFactory(_baseModeFactory, _trailerModeParametersProvider, _gearShifter, _externalSystems);
    }
}