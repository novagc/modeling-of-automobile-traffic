namespace AutomobileTrafficModeling.Core.Generator.Data
{
    public struct TimesToNextCars
    {
        public readonly short Up;
        public readonly short Down;
        public readonly short Left;
        public readonly short Right;

        /// <summary>
        /// If the time is less than 0, then the cars will not appear <br/>
        /// If the time is 0, then the cars will appear every turn
        /// </summary>
        public TimesToNextCars(short up = -1, short down = -1, short left = -1, short right = -1)
        {
            Up = (short)(up + 1);
            Down = (short)(down + 1);
            Left = (short)(left + 1);
            Right = (short)(right + 1);
        }
    }
}
