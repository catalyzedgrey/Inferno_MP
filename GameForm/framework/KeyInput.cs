using GameForm.objects;
using GameForm.window;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameForm.framework
{

    public class KeyInput
    {

        Handler handler;
        //private HUD hud;

        Game game;

        private bool[] keyDown = new bool[4];
        private bool[] keyDown2 = new bool[4];


        public KeyInput(Game game, Handler handler)
        {
            this.handler = handler;
            this.game = game;

            for (int i = 0; i < 4; i++)
                keyDown[i] = false;
        }

        public void keyPressed(Keys k)
        {
            Keys key = k;


            for (int i = 0; i < Handler.gameObject.Count; i++)
            {
                GameObject tempObject = Handler.gameObject.ElementAt(i);
                if (tempObject.getId() == ObjectID.Player)
                {
                    //Player p1 = (Player) tempObject;
                    if (key == Keys.D)// && Player.canMoveRight) {
                    {
                        tempObject.DX = 5;
                        // HUD.velX=(int)tempObject.getVelX();
                        keyDown[2] = true;

                        // Player.canMoveLeft = true;
                    } //

                    if (key == Keys.A)// && Player.canMoveLeft)
                    {
                        tempObject.DX = -5;
                        keyDown[3] = true;
                        // HUD.velX=(int)tempObject.getVelX();
                        // Player.canMoveRight = true;

                    }


                    if (key == Keys.W && !tempObject.Jumping)
                    {
                        keyDown[0] = true;
                        tempObject.Jumping = true;
                        tempObject.DY = -10;
                    }

                    if (key == Keys.T)
                    {
                        if (GameObject.debugDrawings) { 
                            GameObject.debugDrawings = false;
                            Player.god = false;

                        }
                        else { 
                            GameObject.debugDrawings = true;
                            Player.god = true;
                        }
                    }
                }
            }
        }
        public void keyReleased(Keys k)
        {
            Keys key = k;
            for (int i = 0; i < Handler.gameObject.Count; i++)
            {
                GameObject tempObject = Handler.gameObject.ElementAt(i);
                if (tempObject.getId() == ObjectID.Player)
                {
                    if (key == Keys.D)
                    {
                        keyDown[2] = false;
                    }
                    if (key == Keys.A)
                    {

                        keyDown[3] = false;
                    }

                    if (!keyDown[2] && !keyDown[3])
                        tempObject.DX = 0;

                }


            }

        }

    }

}