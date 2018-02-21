using GameForm.framework;
using GameForm.window;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace GameForm.objects
{
    class PowerOrb : GameObject
    {
        Boolean available = true;
        private int width = 20, height = 20;
        Bitmap animatedImage = new Bitmap(@"resources/PowerOrb.png");

        public PowerOrb(float x, float y, ObjectID id) : base(x, y, id)
        {

        }

        public override void tick(List<GameObject> tempObject)
        {

        }

        public override void render(Graphics g)
        {
            if (available)
            {
                ImageAnimator.UpdateFrames();
                g.DrawImage(this.animatedImage, new Point((int)x, (int)y));

                if (debugDrawings)
                    g.DrawRectangle(new Pen(Color.Magenta), getBounds());
            }
        }
        public override Rectangle getBounds()
        {
            return new Rectangle((int)x, (int)y, (int)width, (int)height);
        }
        public void hideOrb()
        {
            this.available = false;
        }
        public Boolean orbIsAvailable()
        {
            return this.available;
        }
    }
}
