namespace GearBoxDriver.Infrastructure.External
{
    public class Lights
    {
        private int _position;

        /*
         *  null - brak opcji w samochodzie
         *  1-3 - w dół
         *  7-10 - w górę
         */

        public void SetPosition(int position)
        {
            _position = position;
        }

        public int GetLightsPosition()
        {
            return _position;
        }
    }
}