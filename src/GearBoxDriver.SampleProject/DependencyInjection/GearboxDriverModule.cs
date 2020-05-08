using Autofac;
using GearboxDriver.Domain;
using GearboxDriver.Domain.AggressiveModes.Factories;
using GearboxDriver.Domain.DriveModes.Factories;
using GearboxDriver.Domain.GearBoxStates.Factories;
using GearBoxDriver.Infrastructure.External;
using GearBoxDriver.Infrastructure.Facade;

namespace GearBoxDriver.SampleProject.DependencyInjection
{
    public class GearboxDriverModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SoundModule>()
                .AsSelf()
                .SingleInstance();

            builder.RegisterType<GearShifter>()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterType<GearBoxFacade>()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterType<ExternalSystemsFacade>()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterType<GearBoxStateFactory>()
                .AsImplementedInterfaces()
                .SingleInstance();


            builder.RegisterType<AggressiveModeFactory>()
                .AsImplementedInterfaces()
                .SingleInstance();


            builder.RegisterType<DriveModeFactory>()
                .AsImplementedInterfaces()
                .SingleInstance();

            //builder.RegisterType<MDynamicsModeFactory>()
            //    .AsImplementedInterfaces()
            //    .SingleInstance();

            //builder.RegisterType<TrailerModeFactory>()
            //    .AsImplementedInterfaces()
            //    .SingleInstance();

            builder.RegisterDecorator<MDynamicsModeFactory, IDriveModeFactory>();
            builder.RegisterDecorator<TrailerModeFactory, IDriveModeFactory>();

            builder.RegisterType<Dashboard>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
