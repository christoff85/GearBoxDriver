namespace GearboxDriver.Domain.DriveModes.Providers
{
    public interface ITrailerModeParametersProvider
    {
        bool IsTrailerAttached { get; }
    }
}