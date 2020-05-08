namespace GearboxDriver.Domain.ValueObjects
{
    public class SoundVolume
    {
        public int Db { get; }

        public SoundVolume(int db)
        {
            Db = db;
        }
    }
}