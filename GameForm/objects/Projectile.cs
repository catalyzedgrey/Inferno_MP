using GameForm.framework;
using GameForm.window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GameForm.objects
{
    class Projectile : GameObject
    {
        Texture tex = Game.tex;

        public Projectile(float x, float y, ObjectID id)
            : base(x, y, id)
        {

            dy = 5;
        }

        public override void tick(List<GameObject> tempObject)
        {
            x += dx;
            y += dy;
            if (y <= 0 || y >= 725)
                dy *= -1;


        }

        public override void render(Graphics g)
        {
            if (dy < 0)
                g.DrawImage(tex.fireball[0], (int)x, (int)y);
            if (dy > 0)
                g.DrawImage(tex.fireball[1], (int)x, (int)y);

            if (debugDrawings)
                g.DrawRectangle(new Pen(Color.Black), getBounds());
        }

        public override Rectangle getBounds()
        {
            return (new Rectangle((int)x + 15, (int)y + 10, 10, 60));
        }
    }
}
