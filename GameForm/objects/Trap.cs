using GameForm.framework;
using GameForm.window;
using System;
using System.Collections.Generic;
using System.Drawing;


namespace GameForm.objects
{

    class Trap : GameObject
    {

        private int width = 96, height = 59;
        //private Animation bite;
        Bitmap animatedImage = new Bitmap(@"resources/bear_3.gif");
        bool currentlyAnimating = false;

        int bitingState = 0;

        int bitinval = 0;


        Texture tex = Game.tex;
        public Trap(float x, float y, ObjectID id)
            : base(x, y, id)
        {
            // bite = new Animation(5, tex.bearTraparr[0], tex.bearTraparr[1], tex.bearTraparr[2], tex.bearTraparr[3], tex.bearTraparr[4], tex.bearTraparr[5],
            //   tex.bearTraparr[6], tex.bearTraparr[7], tex.bearTraparr[8], tex.bearTraparr[9], tex.bearTraparr[10], tex.bearTraparr[11], tex.bearTraparr[12]);
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

        private void OnFrameChanged(object o, EventArgs e)
        {
            bitinval++;

            if (bitinval == 10)
                bitingState = 1;

            else if (bitinval == 11)
                bitingState = 2;

            else if (bitinval == 12)
                bitingState = 0;

            else if (bitinval > 12)
                bitinval = 0;
        }

        public override void tick(List<GameObject> tempObject)
        {

        }

        public override void render(Graphics g)
        {
            // g.DrawRectangle(new Pen(Color.Green), new Rectangle((int)x, (int)y, width, height));
            //bite.drawAnimation(g, (int)x, (int)y, 192, 79);

            AnimateImage();
            ImageAnimator.UpdateFrames();
            g.DrawImage(this.animatedImage, new Point((int)x, (int)y - 25));

            if (debugDrawings)
                g.DrawRectangle(new Pen(Color.Magenta), getBounds());

        }

        public override Rectangle getBounds()
        {
            if (bitingState == 0)
                return new Rectangle((int)((int)x + 5), (int)((int)y + 10),
                        (int)width - 5, (int)(height / 2) - 20);

            else if (bitingState == 2)
                return new Rectangle((int)((int)x + (width / 2) - ((width / 2) / 2)), (int)((int)y - 20),
                    (int)width / 2, (int)(height / 2) + 10);

            else
                return new Rectangle((int)((int)x + 10 + (width / 2) - ((width / 2) / 2)), (int)((int)y - 20),
                    (int)(width / 2) - 20, (int)(height / 2) + 10);
        }
    }
}