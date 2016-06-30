using System.Drawing;

namespace ViewNS
{
    public interface IView
    {
        void DrawSensors(ref Bitmap bmp);
        void DrawTarget(ref Bitmap bmp);
        void DrawResults(ref Bitmap bmp);
        void DrawGrid(ref Bitmap bmp);
    }
}
