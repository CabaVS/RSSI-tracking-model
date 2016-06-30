using System;
using System.Drawing;

namespace ModelNS
{
    public sealed class Model
    {
        public int XCount { get; private set; }
        public int YCount { get; private set; }

        public bool Pause { get; set; }

        public Environment @Environment { get; private set; }
        public Sensor[][] SensorsList { get; private set; }
        public AbstractTarget Target { get; private set; }
        
        public Tuple<int, int> TargetCell { get; private set; }
        public Point TargetPosition { get; private set; }

        public Model(int xCount, int yCount, int xSize, int ySize)
        {
            XCount = xCount;
            YCount = yCount;

            Pause = false;
            
            Environment = new Environment();

            SensorsList = new Sensor[yCount][];
            for (int i = 0; i < yCount; i++)
            {
                SensorsList[i] = new Sensor[xCount];
                for (int j = 0; j < xCount; j++)
                    SensorsList[i][j] = new Sensor(new Point(j * xSize / xCount, i * ySize / yCount));
            }

            Target = new LinearMovingTarget(xSize, ySize);

            TargetCell = null;
            TargetPosition = Point.Empty;
        }

        public void Process()
        {
            TargetProcess();
            LocateTargetProcess();
        }

        private void LocateTargetProcess()
        {
            TargetCell = FindTargetCell();
            TargetPosition = CalculateTargetLocation(TargetCell);
        }

        private void TargetProcess()
        {
            if (Target.MoveTarget())
                Target = new LinearMovingTarget(SensorsList[YCount - 1][XCount - 1].Coordinates.X, SensorsList[YCount - 1][XCount - 1].Coordinates.Y);

            Target.SendSignal(Environment, SensorsList);
        }

        private Tuple<int, int> FindTargetCell()
        {
            double maxRSSI = -1d;
            Tuple<int, int> maxRSSICell = null;

            for (int i = 2; i < YCount; i += 2)
                for (int j = 2; j < XCount; j += 2)
                {
                    double rssi = SensorsList[i - 2][j - 2].RSSIval + SensorsList[i - 2][j].RSSIval + SensorsList[i][j - 2].RSSIval + SensorsList[i][j].RSSIval;

                    if (rssi > maxRSSI)
                    {
                        maxRSSI = rssi;
                        maxRSSICell = new Tuple<int, int>(i, j);
                    }
                }

            return maxRSSICell;
        }

        private Point CalculateTargetLocation(Tuple<int, int> targetCell)
        {
            Sensor s1 = SensorsList[targetCell.Item1][targetCell.Item2 - 2];
            Sensor s2 = SensorsList[targetCell.Item1][targetCell.Item2];
            Sensor s3 = SensorsList[targetCell.Item1 - 2][targetCell.Item2];
            Sensor s4 = SensorsList[targetCell.Item1 - 2][targetCell.Item2 - 2];

            int x1 = s1.Coordinates.X;
            int y1 = s1.Coordinates.Y;
            int x2 = s3.Coordinates.X;
            int y2 = s3.Coordinates.Y;

            double d1 = CalculateDistance(s1.RSSIval);
            double d2 = CalculateDistance(s2.RSSIval);
            double d3 = CalculateDistance(s3.RSSIval);
            double d4 = CalculateDistance(s4.RSSIval);

            int x = (int)(0.5 * (x1 + x2 - ((d1 * d1 + d4 * d4) - (d2 * d2 + d3 * d3)) / (2 * (x1 - x2))));
            int y = (int)(0.5 * (y1 + y2 - ((d1 * d1 + d2 * d2) - (d3 * d3 + d4 * d4)) / (2 * (y1 - y2))));

            return new Point(x, y);
        }

        private double CalculateDistance(double RSSIval)
        {
            return Environment.ReferenceDistance * Math.Pow(10, (Environment.ReferenceRSSIVal - RSSIval) / (10 * Environment.PathLossCoeficient));
        }
    }
}
