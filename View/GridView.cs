using System.Drawing;

namespace ViewNS
{
    public static class GridView
    {
        public static bool Visible { get; set; }
        public static int Width { get; set; }

        public static Color @Color { get; set; }

        static GridView()
        {
            Visible = false;
            Width = 1;

            Color = Color.Black;
        }
    }
}
