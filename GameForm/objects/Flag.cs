using GameForm.framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameForm.objects
{
    class Flag : GameObject
    {
        Bitmap animatedImage = new Bitmap(@"resources/portal.gif");
        bool currentlyAnimating = false;
        PointF p;

        public Flag(float x, float y, ObjectID id)
            : base(x, y, id)
        {
            p = new PointF(x, y);
        }


        public override void render(Graphics g)
        {
            AnimateImage();
            ImageAnimator.UpdateFrames();
            g.DrawImage(this.animatedImage, p.X, p.Y - 28);

            if (debugDrawings)
                g.DrawRectangle(new Pen(Color.Red, 5), getBounds());

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
            //Force a call to the Paint event handler.
            // this.Invalidate();
        }


        public override void tick(List<GameObject> tempObject)
        {
            //throw new NotImplementedException();
        }

        public override Rectangle getBounds()
        {
            return new Rectangle((int)x+30, (int)y+10, 60, 50);
        }
    }
}
