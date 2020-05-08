using Autofac;
using GearboxDriver.Domain.AggressiveModes;
using GearboxDriver.Domain.DriveModes;
using GearboxDriver.Domain.GearBoxStates;
using GearboxDriver.Domain.GearBoxStates.Factories;
using GearboxDriver.Domain.ValueObjects;
using GearBoxDriver.Infrastructure.External;
using GearBoxDriver.SampleProject.DependencyInjection;

namespace GearBoxDriver.SampleProject
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = BuildContainer();

            var dashboard = container.Resolve<IDashboard>();
            
            dashboard.SetGearState(GearBoxStateValue.DriveState);
            dashboard.SetDriveMode(DriveModeValue.ComfortMode);
            dashboard.SetAggresiveMode(AggressiveModeValue.AggresiveMode2);
            dashboard.SetMDynamics(true);
            dashboard.SetTrailerAttached(true);

            var gearBoxStateFactory = container.Resolve<IGearBoxStateFactory>();
            var state = gearBoxStateFactory.Create();

            state.ManualUpshift();
            state.ManualUpshift();

            var threshold = new Threshold(0.4d);
            state.Accelerate(threshold);
        }

        private static IContainer BuildContainer()
        {
            var externalSystems = SetUpExternalSystems();
            var gearBox = SetUpGearBox();

            var builder = new ContainerBuilder();
            builder.RegisterModule<GearboxDriverModule>();
            builder.Register(c => externalSystems)
                .AsSelf()
                .SingleInstance();
            builder.Register(c => gearBox)
                .AsSelf();

            return builder.Build();
        }

        private static ExternalSystems SetUpExternalSystems()
        {
            var externalSystems = new ExternalSystems
            {
                CurrentRpm = 6000d,
            };

            externalSystems.Lights.SetPosition(3);

            return externalSystems;
        }

        private static Gearbox SetUpGearBox()
        {
            var gearBox = new Gearbox();
            gearBox.SetMaxDrive(5);

            return gearBox;
        }
    }
}