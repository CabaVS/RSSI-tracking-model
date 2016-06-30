using System;
using System.Drawing;

namespace ModelNS
{
    public sealed class Sensor
    {
        public Point Coordinates { get; private set; }
        public double RSSIval { get; private set; }

        public Sensor(Point coordinates)
        {
            Coordinates = coordinates;
            RSSIval = -1d;
        }

        public void ReceiveSignal(Environment environment, Point sourceCoordinates)
        {
            var realDistance = Math.Sqrt(Math.Pow(sourceCoordinates.X - Coordinates.X, 2) + Math.Pow(sourceCoordinates.Y - Coordinates.Y, 2));

            RSSIval = environment.ReferenceRSSIVal - 10 * environment.PathLossCoeficient * Math.Log10(realDistance / environment.ReferenceDistance);
        }
    }
}
