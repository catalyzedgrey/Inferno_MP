using GameForm.framework;
using GameForm.window;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace GameForm.objects
{
    class Trap2 : GameObject
    {
        private int width = 40, height = 32;
        Bitmap animatedImage = new Bitmap(@"resources/Spikes.gif");
        Texture tex = Game.tex;

        public Trap2(float x, float y, ObjectID id)
            : base(x, y, id)
        {

        }

        public override void tick(List<GameObject> tempObject)
        {

        }

        public override void render(Graphics g)
        {
           ImageAnimator.UpdateFrames();
            g.DrawImage(this.animatedImage, new Point((int)x, (int)y));

            if (debugDrawings)
                g.DrawRectangle(new Pen(Color.Magenta), getBounds());

        }

        public override Rectangle getBounds()
        {
            return new Rectangle((int)x, (int)y+5, (int)width, (int)height);
        }
    }
}

