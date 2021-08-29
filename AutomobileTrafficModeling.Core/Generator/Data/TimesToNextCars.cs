namespace AutomobileTrafficModeling.Core.Generator.Data
{
    public struct TimesToNextCars
    {
        public readonly short Up;
        public readonly short Down;
        public readonly short Left;
        public readonly short Right;

        /// <summary>
        /// If the time is less than 0, then the cars will not appear
        /// </summary>
        public TimesToNextCars(short up = -1, short down = -1, short left = -1, short right = -1)
        {
            Up = up;
            Down = down;
            Left = left;
            Right = right;
        }
    }
}
