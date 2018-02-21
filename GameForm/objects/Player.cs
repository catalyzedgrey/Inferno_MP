using System;
using System.Collections.Generic;
using System.Linq;
using GameForm.framework;
using System.Drawing;
using GameForm.window;
using System.Threading;
using System.Windows;

namespace GameForm.objects
{
    [Serializable]
    public class State
    {
        public float x, y;
        public int facing;
        public float dx;
        public bool Dead;
        public int DeathReason;//1:boulder,2:trap,3:fire, 4 FINISH, 5: trap2 (spikes)
        public string plyName;

        public State(float x, float y, int facing, float dx, bool Dead)
        {
            this.x = x;
            this.y = y;
            this.facing = facing;
            this.dx = dx;
            this.Dead = Dead;
        }
        public State(float x, float y, bool Dead, int wp)
        {
            this.x = x;
            this.y = y;
            this.Dead = Dead;
            DeathReason = wp;
        }

        public State(string a)
        {
            plyName = a;
        }
    }

    public class DeathParticle
    {
        public Bitmap particle;
        private bool bloodCurrentlyAnimating = false;
        public PointF deathPos;
        private int bloodFrames;

        public DeathParticle(PointF d, int DeathReason)
        {

            if (DeathReason == 3)
                particle = new Bitmap(@"resources\explode.gif");
            else
                particle = new Bitmap(@"resources\blood1.gif");

            bloodFrames = 0;
            deathPos = d;
        }
        private void OnFrameChanged(object sender, EventArgs e)
        {
            bloodFrames++;
            if (bloodFrames == 8)
                try
                {
                    Player.DeathParticles.Remove(this);
                }
                catch (Exception ex) { Console.WriteLine(ex.StackTrace.ToString()); }
        }
        public void AnimateBlood()
        {
            if (!bloodCurrentlyAnimating)
            {
                //Begin the animation only once.
                ImageAnimator.Animate(particle, new EventHandler(this.OnFrameChanged));
                bloodCurrentlyAnimating = true;
            }
        }
    }

    public class Player : GameObject
    {
        private float width = 32, height = 44, p2w = 48, p2h = 48;
        protected float gravity = 0.75f;
        private const float MAX_SPEED = 10;
        public bool check = true;

        private float origX, origY;
        public static bool god = false;

        public static List<DeathParticle> DeathParticles = new List<DeathParticle>(10);
        public int DeathReason;

        public static string playerName;
        public static string player2Name;
        private static string WINNER;
        private int stringdx = 1;

        SolidBrush brush = new SolidBrush(Color.Red);
        Font font = new Font("Microsoft Sans Serif", 12);

        private Handler handler;
        private Camera cam;
        Game game;
        Texture tex = Game.tex;
        private Animation playerWalk, playerWalkLeft, p2Walk, p2WalkLeft;

        public static Thread t;
        public static Thread t2;
        public Player(float x, float y, Game game, Handler handler, Camera cam, ObjectID id)
             : base(x, y, id)
        {

            if (id == ObjectID.Player)
            {
                this.game = game;
                this.handler = handler;
                this.cam = cam;

                origX = x;
                origY = y;

                playerWalk = new Animation(1, tex.player[1], tex.player[2], tex.player[3], tex.player[4]);
                playerWalkLeft = new Animation(1, tex.player[5], tex.player[6], tex.player[7], tex.player[8]);
            }

            else if (id == ObjectID.Player2)
            {
                p2Walk = new Animation(1, tex.player2[1], tex.player2[2], tex.player2[3], tex.player2[4]);
                p2WalkLeft = new Animation(1, tex.player2[5], tex.player2[6], tex.player2[7], tex.player2[8]);

                t = new Thread(p2StateListener);
                t2 = new Thread(p2TCP_StateListener);


            }
        }

        public void p2TCP_StateListener()
        {
            while (true)
            {
                if (Network.Connected && Game.gameState == Game.STATE.Game)
                {
                    State a = Network.getTCP();

                    if (a.plyName != null)
                        player2Name = a.plyName;

                    if (a.DeathReason == 4)
                        endMatch(2);

                    else if (a.Dead)
                        DeathParticles.Add(new DeathParticle(new PointF(a.x, a.y), a.DeathReason));
                }
            }
        }

        public void p2StateListener()
        {
            while (true)
            {
                if (Network.Connected && Game.gameState == Game.STATE.Game)
                {

                    State a = Network.getUDP();

                    foreach (GameObject obj in Handler.gameObject)
                        if (id == ObjectID.Player2)
                        {
                            facing = a.facing;
                            dx = a.dx;
                            x = a.x;
                            y = a.y;
                        }
                }
            }
        }

        public override void tick(List<GameObject> tempObject)
        {
            if (this.id == ObjectID.Player)
            {
                x += dx;
                y += dy;

                if (dx < 0)
                    facing = -1;
                else if (dx > 0)
                    facing = 1;

                if (falling || jumping)
                {
                    dy += gravity;
                    Collision(tempObject);
                    if (dy > MAX_SPEED)
                        dy = MAX_SPEED;
                }

                playerWalk.runAnimation();
                playerWalkLeft.runAnimation();

                if (y > 725)
                {
                    this.gravity = 0.75f;
                    this.Dead = true;
                }

                if (this.Dead && !god)
                {
                    DeathParticles.Add(new DeathParticle(new PointF(x, y), DeathReason));

                    if (Network.Connected)
                        Network.sendTCP(new State(x, y, true, DeathReason));

                    resetPos();

                    this.Dead = false;
                }
                else
                {
                    if (Network.Connected)
                        Network.sendUDP(new State(x, y, facing, dx, Dead));
                }

            }
            #region player 2
            else if (this.id == ObjectID.Player2)
            {
                p2WalkLeft.runAnimation();
                p2Walk.runAnimation();
            }
            #endregion
        }

        public override void render(Graphics g)
        {
            
            if (WINNER != null)
                g.DrawString(WINNER + "\nwon!", new Font("Arial", 100), new SolidBrush(Color.Red), stringdx++, 50);

            for (int i = 0; i < DeathParticles.Count; i++)
            {
                try
                {
                    DeathParticles[i].AnimateBlood();
                    ImageAnimator.UpdateFrames();
                    g.DrawImage(DeathParticles[i].particle, DeathParticles[i].deathPos.X - this.width * 2,
                        DeathParticles[i].deathPos.Y - this.height);
                }
                catch (Exception e) { Console.WriteLine(e.StackTrace.ToString()); }
            }

            #region p1
            if (this.id == ObjectID.Player)
            {

                if (god)
                {
                    g.DrawEllipse(new Pen(new SolidBrush(Color.Red), 7), x,y,35,50);
                }

                if(this.gravity == 0.375f)
                {
                    g.DrawEllipse(new Pen(new SolidBrush(Color.Blue), 7), x, y, 35, 50);
                }

                if (dx != 0)
                {
                    if (facing == 1)
                        playerWalk.drawAnimation(g, (int)x, (int)y, 32, 44);

                    else
                        playerWalkLeft.drawAnimation(g, (int)x, (int)y, 32, 44);
                }
                else
                    g.DrawImage(tex.player[0], (int)x, (int)y, 32, 44);

                if (debugDrawings)
                {
                    g.DrawRectangle(new Pen(Color.Gray), (int)x, (int)y, 32, 44);
                    g.DrawRectangle(new Pen(Color.AliceBlue), getBounds());
                    g.DrawRectangle(new Pen(Color.DarkGreen), getBoundsTop());
                    g.DrawRectangle(new Pen(Color.Red), getBoundsRight());
                    g.DrawRectangle(new Pen(Color.Silver), getBoundsLeft());
                }
                g.DrawString(playerName, font, brush, new PointF(x - 5, y - 15));
            }
            #endregion
            else if (this.id == ObjectID.Player2)
            {
                if (debugDrawings)
                {
                    g.DrawRectangle(new Pen(Color.Gray), (int)x, (int)y, 48, 48);
                    g.DrawRectangle(new Pen(Color.AliceBlue), getBounds());
                    g.DrawRectangle(new Pen(Color.DarkGreen), getBoundsTop());
                    g.DrawRectangle(new Pen(Color.Red), getBoundsRight());
                    g.DrawRectangle(new Pen(Color.Silver), getBoundsLeft());
                }
                if (dx != 0)
                {
                    if (facing == 1)
                        p2Walk.drawAnimation(g, (int)x, (int)y, 48, 48);

                    else if (facing == -1)
                        p2WalkLeft.drawAnimation(g, (int)x, (int)y, 48, 48);
                }
                else
                    g.DrawImage(tex.player2[0], (int)x, (int)y, 48, 44);

                g.DrawString(player2Name, font, brush, new PointF(x - 5, y - 15));
            }
        }



        private void Collision(List<GameObject> obj)
        {

            for (int i = 0; i < Handler.gameObject.Count; i++)
            {
                GameObject tempObject = Handler.gameObject.ElementAt(i);

                #region block
                if (tempObject.getId() == ObjectID.Block)
                {
                    if (((Block)tempObject).getType() == 0 || ((Block)tempObject).getType() == 1)
                    {
                        //top
                        if (getBoundsTop().IntersectsWith(tempObject.getBounds()))
                        {
                            y = tempObject.Y + (44);
                            dy = 0;
                        }
                        //bottom
                        if (getBounds().IntersectsWith(tempObject.getBounds()))
                        {
                            y = tempObject.Y - height;
                            dy = 0;
                            falling = false;
                            jumping = false;
                        }
                        else
                            falling = true;

                        // Right
                        if (getBoundsRight().IntersectsWith(tempObject.getBounds()))
                        {
                            x = tempObject.X - width;
                        }
                        // Left
                        if (getBoundsLeft().IntersectsWith(tempObject.getBounds()))
                        {
                            x = tempObject.X + width;
                        }
                    }
                }
                #endregion
                else if (tempObject.getId() == ObjectID.Boulder)
                {
                    Boulder b = (Boulder)tempObject;

                    if (b.C.IntersectsWith(this.getBounds(), true || b.C.IntersectsWith(this.getBoundsTop(), true)))
                    {
                        this.gravity = 0.75f;
                        if(!god)
                        Dead = true;
                        DeathReason = 1;
                    }
                }
                else if (tempObject.getId() == ObjectID.Flag)
                {
                    if (this.getBounds().IntersectsWith(tempObject.getBounds()))
                        thisPlayerWon();
                }
                else if (tempObject.getId() == ObjectID.Trap)
                {
                    if (this.getBounds().IntersectsWith(tempObject.getBounds()) || this.getBoundsTop().IntersectsWith(tempObject.getBounds()))
                    {
                        this.gravity = 0.75f;
                        if (!god)
                            Dead = true;
                        DeathReason = 2;
                    }
                }
                else if (tempObject.getId() == ObjectID.Trap2)
                {
                    if (this.getBounds().IntersectsWith(tempObject.getBounds()) || this.getBoundsTop().IntersectsWith(tempObject.getBounds()))
                    {
                        this.gravity = 0.75f;
                        if (!god)
                            Dead = true;
                        DeathReason = 5;
                    }
                }
                else if (tempObject.getId() == ObjectID.Projectile)
                {
                    if (this.getBounds().IntersectsWith(tempObject.getBounds()) || this.getBoundsTop().IntersectsWith(tempObject.getBounds()))
                    {
                        this.gravity = 0.75f;
                        if (!god)
                            Dead = true;
                        DeathReason = 3;
                    }
                }
                //Jump Orb (double jumb ability)
                else if (tempObject.getId() == ObjectID.JumpOrb)
                {
                    JumpOrb jo = (JumpOrb)tempObject;
                    if (this.getBounds().IntersectsWith(tempObject.getBounds()) && jo.orbIsAvailable())
                    {
                        this.gravity = 0.375f;
                        jo.hideOrb();
                    }
                }
                //Hole collision
                else if (tempObject.getId() == ObjectID.Hole)
                {
                    if (this.getBounds().IntersectsWith(tempObject.getBounds()) || this.getBoundsTop().IntersectsWith(tempObject.getBounds()))
                        if (!god)
                            Dead = true;
                }
                //death_rasengan Collision
                else if (tempObject.getId() == ObjectID.death_rasengan)
                {
                    if (this.getBounds().IntersectsWith(tempObject.getBounds()) || this.getBoundsTop().IntersectsWith(tempObject.getBounds()))
                        dy -= 20;

                }
                //Power Orb (invincibility)
                else if (tempObject.getId() == ObjectID.PowerOrb)
                {
                    PowerOrb po = (PowerOrb)tempObject;
                    if (this.getBounds().IntersectsWith(tempObject.getBounds()) && po.orbIsAvailable())
                    {
                        po.hideOrb();
                        Player.god = true;
                        new Thread(Ttt_Elapsed).Start();
                        
                    }
                }
                //SpeedBall
                /* else if (tempObject.getId()==Object ID.SpeedBall) {
                    if (this.getBoundsTop().IntersectsWith(tempObject.getBounds())|| this.getBounds().IntersectsWith(tempObjects.getBounds()))
                    dx+=3             */
            }
        }
        private void Ttt_Elapsed()
        {
            Thread.Sleep(5000);
            Player.god = false;
        }

        private void thisPlayerWon()
        {
            Network.sendTCP(new State(x, y, false, 4));
            //resetPos();//tto end game once
            endMatch(1);
        }
        private void endMatch(int player)
        {
            Console.WriteLine("MATCH END");

            if (player == 1)
                WINNER = playerName;
            else if (player == 2)
                WINNER = player2Name;
            
            new Thread(returnToMenuDelayed).Start();
        }

        private void returnToMenuDelayed()
        {
            Thread.Sleep(5000);
            Environment.Exit(1);

            /*  Game.gameState = Game.STATE.Menu;
             Network.closeClientNetwork();
             WINNER = null;
             stringdx = 1;
             resetPos();*/
        }

        public void resetPos()
        {
            this.x = origX;
            this.y = origY - 15;
        }
        public override Rectangle getBounds()
        {
            if (id == ObjectID.Player)
                return new Rectangle((int)((int)x + (width / 2) - (width / 2) / 2), (int)((int)y + (height / 2)), (int)width / 2, (int)height / 2);
            return new Rectangle((int)((int)x + (p2w / 2) - (p2w / 2) / 2), (int)((int)y + (p2h / 2)), (int)p2w / 2, (int)p2h / 2);
        }

        public Rectangle getBoundsTop()
        {
            if (id == ObjectID.Player)
                return new Rectangle((int)((int)x + (width / 2) - (width / 2) / 2), (int)y, (int)width / 2, (int)height / 2);
            return new Rectangle((int)((int)x + (p2w / 2) - (p2w / 2) / 2), (int)y, (int)p2w / 2, (int)p2h / 2);
        }

        public Rectangle getBoundsRight()
        {
            if (id == ObjectID.Player)
                return new Rectangle((int)((int)x + width - 5), (int)y + 5, (int)5, (int)height - 10);
            return new Rectangle((int)((int)x + p2w - 5), (int)y + 5, (int)5, (int)p2h - 10);
        }

        public Rectangle getBoundsLeft()
        {
            if (id == ObjectID.Player)
                return new Rectangle((int)x, (int)y + 5, (int)5, (int)height - 10);
            return new Rectangle((int)x, (int)y + 5, (int)5, (int)p2h - 10);
        }
    }
}