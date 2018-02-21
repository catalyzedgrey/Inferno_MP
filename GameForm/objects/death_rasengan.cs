using GameForm.framework;
using GameForm.window;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;


namespace GameForm.objects
{

    class death_rasengan : GameObject
    {

        private int width = 40, height = 40;
        //private Animation bite;
        Bitmap animatedImage = new Bitmap(@"resources\death_rasengan.png");
        bool currentlyAnimating = false;

        bool collided = false;



        Texture tex = Game.tex;
        public death_rasengan(float x, float y, ObjectID id)
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

        /// <summary>
        /// ////////////
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
        private void OnFrameChanged(object o, EventArgs e)
        {
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
            g.DrawImage(this.animatedImage, new Point((int)x - 10, (int)y - 15));

            if (debugDrawings)
            {
                g.DrawEllipse(new Pen(Color.Blue), x, y, width, height);
            }
        }

        public override Rectangle getBounds()
        {
            if (!collided)
                return new Rectangle((int)((int)x + 5), (int)((int)y),
                        (int)width - 5, (int)(height / 2) - 20);

            else
                return new Rectangle((int)((int)x + (width / 2) - ((width / 2) / 2)), (int)((int)y - 20),
                    (int)width / 2, (int)(height / 2) + 10);
        }
    }
}
