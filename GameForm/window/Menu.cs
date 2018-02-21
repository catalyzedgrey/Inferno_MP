using System;
using System.Drawing;
using System.Windows.Forms;

namespace GameForm.window
{
    public class Menu : Panel
    {

        Bitmap animatedImage;
        bool currentlyAnimating = false;
        Pen pen = new Pen(Color.Black);

        private Bitmap loadin;
        bool currentlyAnimatingLoading = false;

        Font f = new Font("Castellar", 50);

        public JoinIP joinipform;
      
        public Menu()
        {
            joinipform = new JoinIP();
            framework.Sounds.LobbySoundPlay();
            animatedImage = new Bitmap(@"resources/background.gif");
            loadin = new Bitmap(@"resources/loader.gif");
        }

        public void AnimateImage()
        {
            if (!currentlyAnimating)
            {
                //Begin the animation only once.
                ImageAnimator.Animate(animatedImage, new EventHandler(this.OnFrameChanged));
                currentlyAnimating = true;
            }
        }

        public void Animateloading()
        {
            if (!currentlyAnimatingLoading)
            {
                ImageAnimator.Animate(loadin, new EventHandler(this.OnFrameChanged));
                currentlyAnimatingLoading = true;
            }
        }

        private void OnFrameChanged(object o, EventArgs e)
        {
            //Force a call to the Paint event handler.
            // this.Invalidate();
        }
        
        public new void Click(MouseEventArgs e)
        {
            int mx = e.X;
            int my = e.Y;

            if (Game.gameState == Game.STATE.Menu)
            {
                if (mouseOver(mx, my, 450, 150, 250, 85))
                { // when click join 
                    new window.NameWindow().ShowDialog();
                    joinipform.ShowDialog();
                }
                if (mouseOver(mx, my, 450, 250, 250, 85))
                { // when click host 

                    new window.NameWindow().ShowDialog();

                    try
                    {
                        Network.TCPconnect("0", true);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace.ToString());
                    }
                }
                if (mouseOver(mx, my, 450, 350, 250, 85))
                {
                    Application.Exit();
                }
            }
        }

        private bool mouseOver(int mx, int my, int x, int y, int width, int height)
        {
            if (mx > x && mx < x + width)
            {
                if (my > y && my < y + height)
                    return true;
                return false;
            }
            else
                return false;
        }

        public void render(Graphics g)
        {
            AnimateImage();
            ImageAnimator.UpdateFrames();
            g.DrawImage(this.animatedImage, new Point(0, 0));

            if (Network.Connecting)
            {
                Animateloading();
                ImageAnimator.UpdateFrames();
                g.DrawImage(this.loadin, 650,70,70,70);
            }

            //g.DrawRectangle(pen, 450, 150, 250, 85);
            //g.DrawRectangle(pen, 450, 250, 250, 85);
            //g.DrawRectangle(pen, 450, 350, 250, 85);
            g.DrawString("Join", f, new SolidBrush(Color.Black), 460, 150);
            g.DrawString("Host", f, new SolidBrush(Color.Black), 450, 250);
            g.DrawString("Exit", f, new SolidBrush(Color.Black), 460, 350);
        }
    }
}