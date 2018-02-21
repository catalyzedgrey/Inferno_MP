using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameForm.framework
{
    public class Texture
    {
        SpriteSheet ps1, bs, fsprite, ps2;//, bt, leftLight, rightLight, leftFire, rightFire, bg, enemysprite, portalSprite, lavaSprite,
                                // flyingEn, bonSprite, icess, boSS, iciss, spikess, chimerass ;

        private Image block_sheet = Image.FromFile(@"resources/block_sheet.png");

        private Image player_sheet = Image.FromFile(@"resources/devil.png");
        private Image player2_sheet = Image.FromFile(@"resources/angel.png");

        private Image fball = Image.FromFile(@"resources/fireball.png");

        public Bitmap[] block = new Bitmap[8];

        public Bitmap[] player = new Bitmap[9];

        public Bitmap[] player2 = new Bitmap[9];

        public Bitmap[] fireball = new Bitmap[2];
        

        public Texture()
        {
            bs = new SpriteSheet((Bitmap)block_sheet);
            ps1 = new SpriteSheet((Bitmap)player_sheet);
            ps2 = new SpriteSheet((Bitmap)player2_sheet);
            fsprite = new SpriteSheet((Bitmap)fball);
            getTextures();
        }

        private void getTextures()
        {
            block[0] = bs.Load(1, 1, 32, 32, "block_sheet.png");
            //--------------------------------------------------------

            //p1
            // idle player
            player[0] = ps1.Load(1, 1, 32, 48, "devil.png");

            // right
            player[1] = ps1.Load(1, 3, 32, 48, "devil.png");
            player[2] = ps1.Load(2, 3, 32, 48, "devil.png");
            player[3] = ps1.Load(3, 3, 32, 48, "devil.png");
            player[4] = ps1.Load(4, 3, 32, 48, "devil.png");

            // left
            player[5] = ps1.Load(1, 2, 32, 48, "devil.png");
            player[6] = ps1.Load(2, 2, 32, 48, "devil.png");
            player[7] = ps1.Load(3, 2, 32, 48, "devil.png");
            player[8] = ps1.Load(4, 2, 32, 48, "devil.png");
            //--------------------------------------------------------

            //p2
            player2[0] = ps2.Load(1, 1, 48, 48, "angel.png");

            //// right
            player2[1] = ps2.Load(1, 3, 48, 48, "angel.png");
            player2[2] = ps2.Load(2, 3, 48, 48, "angel.png");
            player2[3] = ps2.Load(3, 3, 48, 48, "angel.png");
            player2[4] = ps2.Load(4, 3, 48, 48, "angel.png");

            // left
            player2[5] = ps2.Load(1, 2, 48, 48, "angel.png");
            player2[6] = ps2.Load(2, 2, 48, 48, "angel.png");
            player2[7] = ps2.Load(3, 2, 48, 48, "angel.png");
            player2[8] = ps2.Load(4, 2, 48, 48, "angel.png");
            //--------------------------------------------------------

            fireball[0] = fsprite.Load(1, 1, 40, 93, "fireball.png");
            fireball[1] = fsprite.Load(2, 1, 40, 93, "fireball.png");
        }
    }
}
