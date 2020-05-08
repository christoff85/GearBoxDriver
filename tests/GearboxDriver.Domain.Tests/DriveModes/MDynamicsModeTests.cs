using System;
using FluentAssertions;
using GearboxDriver.Domain.DriveModes;
using GearboxDriver.Domain.ValueObjects;
using NSubstitute;
using Xunit;

namespace GearboxDriver.Domain.Tests.DriveModes
{
    public class MDynamicsModeTests
    {
        private readonly AngularSpeed _cutOffAngularSpeed = new AngularSpeed(50d);
        private readonly IDriveMode _baseMode = Substitute.For<IDriveMode>();
        private readonly IExternalSystems _externalSystems = Substitute.For<IExternalSystems>();

        [Fact]
        public void Accelerate_ShouldCallAccelerateOnBaseMode_WhenCurrentAngularSpeedIsLesserThanCutOffAngularSpeed()
        {
            
            var currentAngularSpeed = new AngularSpeed(40d);
            _externalSystems.GetAngularSpeed().Returns(currentAngularSpeed);

            var threshold = new Threshold(0.6d);
            Sut().Accelerate(threshold);

            _baseMode.Received(1).Accelerate(threshold);
        }

        [Fact]
        public void Accelerate_ShouldNotCallAccelerateOnBaseMode_WhenCurrentAngularSpeedIsGreaterThanCutOffAngularSpeed()
        {
            var currentAngularSpeed = new AngularSpeed(60d);
            _externalSystems.GetAngularSpeed().Returns(currentAngularSpeed);

            var threshold = new Threshold(0.6d);
            Sut().Accelerate(threshold);

            _baseMode.DidNotReceive().Accelerate(threshold);
        }

        [Fact]
        public void Ctor_ShouldThrowArgumentNullException_WhenCutOffAngularSpeedIsNull()
        {
            Action act = () => new MDynamicsMode(null, _baseMode, _externalSystems);

            act.Should().Throw<ArgumentNullException>()
                .Which.ParamName.Should().Be("cutOffAngularSpeed");
        }

        [Fact]
        public void Ctor_ShouldThrowArgumentNullException_WhenBaseModeIsNull()
        {
            Action act = () => new MDynamicsMode(_cutOffAngularSpeed, null, _externalSystems);

            act.Should().Throw<ArgumentNullException>()
                .Which.ParamName.Should().Be("baseMode");
        }

        [Fact]
        public void Ctor_ShouldThrowArgumentNullException_WhenExternalSystemsIsNull()
        {
            Action act = () => new MDynamicsMode(_cutOffAngularSpeed, _baseMode, null);

            act.Should().Throw<ArgumentNullException>()
                .Which.ParamName.Should().Be("externalSystems");
        }

        private MDynamicsMode Sut() => new MDynamicsMode(_cutOffAngularSpeed, _baseMode, _externalSystems);
    }
}