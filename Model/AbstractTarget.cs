using System;
using System.Drawing;

namespace ModelNS
{
    public abstract class AbstractTarget
    {
        public Point Position { get; protected set; }

        public AbstractTarget(int maxWidth, int maxHeight)
        {
            var rand = new Random();

            if (rand.Next(0, 2) == 0)
                Position = new Point(0, rand.Next(1, maxHeight));
            else
                Position = new Point(rand.Next(1, maxWidth), 0);
        }

        public abstract bool MoveTarget();

        public abstract void SendSignal(Environment environment, Sensor[][] sensorList);
    }
}
