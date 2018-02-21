using GameForm.framework;
using GameForm.window;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameForm.objects
{
    public class Block : GameObject
    {
        private int type;  //the type of block just determines which tile we can use (dirt, ice, fire, etc)
        Texture tex = Game.tex;
        public Block(float x, float y, int type, ObjectID id)
            : base(x, y, id)
        {

            this.type = type;
            //lavAnim = new Animation(5, tex.block[2], tex.block[3], tex.block[4]);//, tex.block[5], tex.block[6], tex.block[7]);
            //campAnim = new Animation(5, tex.bonfire[0], tex.bonfire[1], tex.bonfire[2], tex.bonfire[3], tex.bonfire[4], tex.bonfire[5], tex.bonfire[6], tex.bonfire[7], tex.bonfire[8]);
            //loader = new BufferedImageLoader();
            //icetile = loader.loadImage("/icetile.png");
            //rightice = loader.loadImage("/rightice.png");
            //leftice = loader.loadImage("/leftice.png");
        }

        public override void tick(List<GameObject> obj)
        {
            
        }


        public override void render(Graphics g)
        {
            /*g.setColor(Color.white);
            g.drawRect((int)x, (int)y, 32, 64);*/

            //check which type to render appropriate tile
            switch (type) 
            {
                case 0:     //dirt
                    g.DrawImage(tex.block[0], new Point((int)x, (int)y));
                    break;

            }
        }

        public override Rectangle getBounds()
        {
            return new Rectangle((int)x, (int)y, 32, 32);
        }

        public int getType()
        {
            return type;
        }



    }
}
