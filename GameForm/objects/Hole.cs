using GameForm.framework;
using GameForm.window;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace GameForm.objects
{

    class Hole : GameObject
    {
        private int width = 40, height = 40;
        //private Animation bite;
        Bitmap animatedImage = new Bitmap(@"resources\hole.png");
        bool currentlyAnimating = false;

        bool collided = false;

        Texture tex = Game.tex;
        public Hole(float x, float y, ObjectID id)
            : base(x, y, id)
        { }

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
        }

        public override void tick(List<GameObject> tempObject)
        {

        }

        public override void render(Graphics g)
        {
            AnimateImage();
            ImageAnimator.UpdateFrames();
            g.DrawImage(this.animatedImage, new Point((int)x, (int)y - 25));

            if (debugDrawings)
            {
                g.DrawRectangle(new Pen(Color.Blue), getBounds());
            }
        }

        public override Rectangle getBounds()
        {
            return new Rectangle((int) x + 45, (int)((int)y - 20),
               80,120);
           
        }
    }
}
