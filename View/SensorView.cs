using System.Drawing;

namespace ViewNS
{
    public static class SensorView
    {
        public static int Width { get; set; }
        public static int Size { get; set; }

        public static Color @Color { get; set; }

        static SensorView()
        {
            Width = 1;
            Size = 4;

            Color = Color.Black;
        }
    }
}
