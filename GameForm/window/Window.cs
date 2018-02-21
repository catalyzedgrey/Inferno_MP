using GameForm.framework;
using GameForm.window;
using System;
using System.Windows.Forms;

namespace GameForm
{
    public partial class Window : Form
    {
        public static Game game;
        KeyInput k;
        public Window()
        {
            InitializeComponent();

            game = new Game();

            game.Dock = DockStyle.Fill;

            this.Controls.Add(game);
            k = new KeyInput(game, Game.h);

            this.KeyDown += Window_KeyDown;
            this.KeyUp += Window_KeyUp;

            this.FormClosed += (sender, e) => {
                Network.closeGameNetwork();
            };
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            k.keyReleased(e.KeyCode);
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            k.keyPressed(e.KeyCode);
        }

        private void Window_Load(object sender, EventArgs e)
        {
            
        }
    }
}
