using System;

using ModelNS;
using ViewNS;

namespace ControllerNS
{
    public sealed class Controller
    {
        public View @View { get; private set; }
        public Model @Model { get; private set; }

        public Controller(int xCount, int yCount)
        {
            View = new View();
            Model = new Model(xCount, yCount, View.XSize, View.YSize);

            View.Model = Model;

            View.Form.ButtonStartClick += Form_ButtonStartClick;
            View.Form.ButtonPauseClick += Form_ButtonPauseClick;
            View.Form.ButtonShowGridClick += Form_ButtonShowGridClick;
        }

        public Controller(Model model, View view)
        {
            Model = model;
            View = view;
        }

        private void Form_ButtonStartClick(object sender, EventArgs e)
        {
            View.Form.ButtonStart.Enabled = false;
            View.Form.ButtonPause.Enabled = true;
            View.Form.ButtonShowGrid.Enabled = true;

            View.StartDrawWSN();
        }

        private void Form_ButtonPauseClick(object sender, EventArgs e)
        {
            Model.Pause = !Model.Pause;
            View.Form.ButtonPause.Text = Model.Pause ? "Continue" : "Pause";
        }

        private void Form_ButtonShowGridClick(object sender, EventArgs e)
        {
            GridView.Visible = !GridView.Visible;
            View.Form.ButtonShowGrid.Text = GridView.Visible ? "Hide grid" : "Show grid";
        }
    }
}
