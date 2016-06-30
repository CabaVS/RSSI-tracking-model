using System.Drawing;
using System.Threading.Tasks;

using ModelNS;

namespace ViewNS
{
    public sealed class View : IView
    {
        public Model @Model { get; set; }
        public Form1 @Form { get; private set; }

        public int XSize { get { return Form.XSize; } }
        public int YSize { get { return Form.YSize; } }

        public View()
        {
            Form = new Form1();
        }

        async public void StartDrawWSN()
        {
            while (true)
            {
                if (Model.Pause)
                {
                    await Task.Delay(50);
                    continue;
                }

                Model.Process();

                var bmp = new Bitmap(XSize, YSize);

                DrawSensors(ref bmp);
                DrawTarget(ref bmp);
                DrawResults(ref bmp);

                if (GridView.Visible)
                    DrawGrid(ref bmp);

                Form.PictureBox.Image = bmp;

                await Task.Delay(50);
            }
        }

        public void DrawSensors(ref Bitmap bmp)
        {
            Graphics g = Graphics.FromImage(bmp);
            Pen pen = new Pen(SensorView.Color, SensorView.Width);

            for (int i = 0; i < Model.SensorsList.Length; i++)
                for (int j = 0; j < Model.SensorsList[0].Length; j++)
                {
                    int start_x = Model.SensorsList[i][j].Coordinates.X - SensorView.Size / 2;
                    int start_y = Model.SensorsList[i][j].Coordinates.Y - SensorView.Size / 2;
                    g.DrawEllipse(pen, start_x, start_y, SensorView.Size, SensorView.Size);
                }
        }

        public void DrawTarget(ref Bitmap bmp)
        {
            Graphics g = Graphics.FromImage(bmp);
            Pen pen = new Pen(TargetView.Color, TargetView.Width);

            g.DrawEllipse(pen, Model.TargetPosition.X - TargetView.Width / 2, Model.TargetPosition.Y - TargetView.Width / 2, TargetView.Size, TargetView.Size);
        }

        public void DrawResults(ref Bitmap bmp)
        {
            Graphics g = Graphics.FromImage(bmp);
            Pen pen = new Pen(ResultsView.Color, ResultsView.Width);

            Point p1 = Model.SensorsList[Model.TargetCell.Item1][Model.TargetCell.Item2].Coordinates;
            Point p2 = Model.SensorsList[Model.TargetCell.Item1][Model.TargetCell.Item2 - 2].Coordinates;
            Point p3 = Model.SensorsList[Model.TargetCell.Item1 - 2][Model.TargetCell.Item2].Coordinates;
            Point p4 = Model.SensorsList[Model.TargetCell.Item1 - 2][Model.TargetCell.Item2 - 2].Coordinates;

            g.DrawLine(pen, p1, Model.TargetPosition);
            g.DrawLine(pen, p2, Model.TargetPosition);
            g.DrawLine(pen, p3, Model.TargetPosition);
            g.DrawLine(pen, p4, Model.TargetPosition);
        }

        public void DrawGrid(ref Bitmap bmp)
        {
            Graphics g = Graphics.FromImage(bmp);
            Pen pen = new Pen(GridView.Color, GridView.Width);

            double x_offset = (double) bmp.Width / Model.XCount;
            double y_offset = (double) bmp.Height / Model.YCount;

            for (int i = 0; i < Model.YCount; i++)
            {
                Point p1 = new Point(0, (int) (i * y_offset));
                Point p2 = new Point(bmp.Width, (int) (i * y_offset));
                g.DrawLine(pen, p1, p2);
            }
            for (int i = 0; i < Model.XCount; i++)
            {
                Point p1 = new Point((int) (i * x_offset), 0);
                Point p2 = new Point((int) (i * x_offset), bmp.Height);
                g.DrawLine(pen, p1, p2);
            }
        }
    }
}
