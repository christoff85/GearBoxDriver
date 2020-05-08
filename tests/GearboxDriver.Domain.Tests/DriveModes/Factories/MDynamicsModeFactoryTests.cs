using System;
using FluentAssertions;
using GearboxDriver.Domain.DriveModes;
using GearboxDriver.Domain.DriveModes.Factories;
using GearboxDriver.Domain.DriveModes.Providers;
using GearboxDriver.Domain.ValueObjects;
using NSubstitute;
using Xunit;

namespace GearboxDriver.Domain.Tests.DriveModes.Factories
{
    public class MDynamicsModeFactoryTests
    {
        private readonly IDriveModeFactory _baseModeFactory = Substitute.For<IDriveModeFactory>();
        private readonly IMDynamicsModeParametersProvider _mDynamicsModeParametersProvider = Substitute.For<IMDynamicsModeParametersProvider>();
        private readonly IExternalSystems _externalSystems = Substitute.For<IExternalSystems>();

        [Fact]
        public void Create_ShouldReturnBaseMode_WhenMDynamicsIsNotEnabled()
        {
            var baseMode = Substitute.For<IDriveMode>();
            _baseModeFactory.Create().Returns(baseMode);
            _mDynamicsModeParametersProvider.IsMDynamicsEnabled.Returns(false);

            var result = Sut().Create();

            result.Should().Be(baseMode);
        }

        [Fact]
        public void Create_ShouldReturnInstanceOfMDynamicsMode_WhenMDynamicsIsEnabled()
        {
            var baseMode = Substitute.For<IDriveMode>();
            _baseModeFactory.Create().Returns(baseMode);
            _mDynamicsModeParametersProvider.IsMDynamicsEnabled.Returns(true);
            _mDynamicsModeParametersProvider.CutOffAngularSpeed.Returns(new AngularSpeed(50d));

            var result = Sut().Create();

            result.Should().NotBe(baseMode);
            result.Should().BeOfType<MDynamicsMode>();
        }
        
        [Fact]
        public void Ctor_ShouldThrowArgumentNullException_WhenBaseModeFactoryIsNull()
        {
            Action act = () => new MDynamicsModeFactory(null, _mDynamicsModeParametersProvider, _externalSystems);

            act.Should().Throw<ArgumentNullException>()
                .Which.ParamName.Should().Be("baseModeFactory");
        }

        [Fact]
        public void Ctor_ShouldThrowArgumentNullException_WhenMDynamicsModeParametersProviderIsNull()
        {
            Action act = () => new MDynamicsModeFactory(_baseModeFactory, null, _externalSystems);

            act.Should().Throw<ArgumentNullException>()
                .Which.ParamName.Should().Be("mDynamicsModeParametersProvider");
        }

        [Fact]
        public void Ctor_ShouldThrowArgumentNullException_WhenExternalSystemsIsNull()
        {
            Action act = () => new MDynamicsModeFactory(_baseModeFactory, _mDynamicsModeParametersProvider, null);

            act.Should().Throw<ArgumentNullException>()
                .Which.ParamName.Should().Be("externalSystems");
        }

        private MDynamicsModeFactory Sut() => new MDynamicsModeFactory(_baseModeFactory, _mDynamicsModeParametersProvider, _externalSystems);
    }
}