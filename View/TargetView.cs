using System.Drawing;

namespace ViewNS
{
    public static class TargetView
    {
        public static int Width { get; set; }
        public static int Size { get; set; }

        public static Color @Color { get; set; }

        static TargetView()
        {
            Width = 4;
            Size = 4;

            Color = Color.Red;
        }
    }
}
