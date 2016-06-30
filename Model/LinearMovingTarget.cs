using System;
using System.Drawing;

namespace ModelNS
{
    public sealed class LinearMovingTarget : AbstractTarget
    {
        public Point StartPosition { get; private set; }
        public Point FinishPosition { get; private set; }

        public int Step { get; private set; }
        public int MaxStep { get; private set; }

        public LinearMovingTarget(int maxWidth, int maxHeight) : base(maxWidth, maxHeight)
        {
            StartPosition = Position;

            Step = 0;
            MaxStep = 100;

            var rand = new Random();

            if (Position.X == 0)
                FinishPosition = new Point(maxWidth, rand.Next(1, maxHeight));
            else
                FinishPosition = new Point(rand.Next(1, maxWidth), maxHeight);
        }

        public LinearMovingTarget(int maxWidth, int maxHeight, int maxStep) : this(maxWidth, maxHeight)
        {
            MaxStep = maxStep;
        }

        public override bool MoveTarget()
        {
            Step++;

            var x = (int)((double)FinishPosition.X - StartPosition.X) / MaxStep * Step + StartPosition.X;
            var y = (int)((double)FinishPosition.Y - StartPosition.Y) / MaxStep * Step + StartPosition.Y;

            Position = new Point(x, y);

            return Step == MaxStep ? true : false;
        }

        public override void SendSignal(Environment environment, Sensor[][] sensorList)
        {
            for (int i = 0; i < sensorList.Length; i++)
                for (int j = 0; j < sensorList[i].Length; j++)
                    sensorList[i][j].ReceiveSignal(environment, Position);
        }
    }
}
