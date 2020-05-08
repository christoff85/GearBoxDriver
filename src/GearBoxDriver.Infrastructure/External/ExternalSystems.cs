namespace GearBoxDriver.Infrastructure.External
{
    public class ExternalSystems
    {
        public double CurrentRpm { get; set; }
        public double AngularSpeed { get; set; }
        public Lights Lights { get; } = new Lights();
    }

    public class SoundModule
    {
        public void MakeSound(int db)
        {

        }
    }
}
