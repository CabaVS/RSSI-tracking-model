using System.Drawing;

namespace ViewNS
{
    public static class ResultsView
    {
        public static int Width { get; set; }

        public static Color @Color { get; set; }

        static ResultsView()
        {
            Width = 1;

            Color = Color.Blue;
        }
    }
}
