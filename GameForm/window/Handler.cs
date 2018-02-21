using GameForm.framework;
using GameForm.objects;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace GameForm.window
{
    public class Handler
    {
        #region fields
        public static List<GameObject> gameObject = new List<GameObject>();
        private GameObject tempObj;
        private Game game;
        private Camera cam;
        
        //private BufferedImageLoader loader;
        //private Details details;
        //private Save save;
        #endregion

        public Handler(Game game, Camera cam)
        {
            this.game = game;
            this.cam = cam;
        }
        public void tick()
        {
            foreach (GameObject obj in gameObject)
                obj.tick(gameObject);
        }
        
        public void render(Graphics g)
        {
            for (int k = 0; k < gameObject.Count; k++)
            {
                tempObj = gameObject.ElementAt(k);

                tempObj.render(g);
            }
        }
        public void LoadImageLevel(Bitmap image)
        {

            int w = image.Width;
            int h = image.Height;

            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    System.Drawing.Color pixel = image.GetPixel(i, j);//getRGB(i, j);
                    int red = pixel.R;//(pixel >> 16) & 0xff;
                    int green = pixel.G; //(pixel >> 8) & 0xff;
                    int blue = pixel.B;// (pixel) & 0xff;
                  
                    if (red == 255 && green == 255 & blue == 255) // white pixel =  dirt
                        addObject(new Block(i * 32, j * 32, 0, ObjectID.Block));

                    if (red == 0 && green == 255 & blue == 33) // trap
                        addObject(new Trap(i * 32, j * 32, ObjectID.Trap));

                    if (red == 0 && green == 255 & blue == 255) // Trap2 (spikes)
                        addObject(new Trap2(i * 32, j * 32, ObjectID.Trap2));

                    if (red == 64 && green == 64 & blue == 64) // boulder
                        addObject(new Boulder(i*32  , j*32 , ObjectID.Boulder));
                    
                    if (red == 0 && green == 0 & blue == 255) // player 1
                        addObject(new Player(i * 32, j * 32, game, this, cam, ObjectID.Player));

                    if (red == 255 && green == 0 & blue == 220) // player 2
                        addObject(new Player(i * 32, j * 32, game, this, cam, ObjectID.Player2));
                    
                    if (red == 87 && green == 0 & blue == 127) // portal
                        addObject(new Flag(i * 32, j * 32, ObjectID.Flag));

                    if (red == 255 && green == 106 & blue == 0) // fire ball projectile
                        addObject(new Projectile(i * 32, j * 32, ObjectID.Projectile));
                   
                      if (red == 255 && green == 255 & blue == 0) // jumb orb for double jumb ability
                          addObject(new JumpOrb(i * 32, j * 32, ObjectID.JumpOrb));

                    if (red == 80 && green == 80 & blue == 0)
                        addObject(new death_rasengan(i * 32, j * 32, ObjectID.death_rasengan));//Adding death_rasengan Object

                    if (red == 80 && green == 0 & blue == 80)
                        addObject(new Hole(i * 32, j * 32, ObjectID.Hole));//Adding Hole Object

                    if (red == 255 && green == 0 & blue == 0) // power orb for invincibility
                        addObject(new PowerOrb(i * 32, j * 32, ObjectID.PowerOrb));
                }
            }
        }

        public void addObject(GameObject obj)
        {
            gameObject.Add(obj);
        }
    }
}
