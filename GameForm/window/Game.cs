using System;
using System.Windows.Forms;
using System.Drawing;
using System.Linq;
using GameForm.framework;
using GameForm.objects;

namespace GameForm.window
{
  /*  public class Utility
    {
        #region Basic Frame Counter

        private static int lastTick;
        private static int lastFrameRate;
        private static int frameRate;

        public static int CalculateFrameRate()
        {
            if (System.Environment.TickCount - lastTick >= 1000)
            {
                lastFrameRate = frameRate;
                frameRate = 0;
                lastTick = System.Environment.TickCount;
            }
            frameRate++;
            return lastFrameRate;
        }
        #endregion
    }
    */

    public partial class Game : Panel
    {
        #region fields
        System.Windows.Forms.Timer ticker;
        public System.Windows.Forms.Timer Ticker { get { return this.ticker; } set { this.ticker = value; } }
        
        public static int WIDTH, HEIGHT;
        public enum STATE { Game, Menu };
        public static STATE gameState = STATE.Menu;
        public Menu menu;

        public static Texture tex;
        private Player p;
        Bitmap animatedImage = new Bitmap(@"resources/bg.gif");
        bool currentlyAnimating = false;
        Camera cam;

        public static int LEVEL = 1;
        public static Handler h;

        #endregion

        public Game()
        {
            this.DoubleBuffered = true;
            this.MouseClick += Game_MouseClick;

            //create game 
            this.start();

            //FOR TESTING SINGLEPLAYER
              //lobbyToGame();
        }

        public void assignName(String a)
        {
            Player.playerName = a;
        }

        public void lobbyToGame() // STARTS GAME
        {
            Game.gameState = Game.STATE.Game;

            framework.Sounds.LobbySoundStop();
            Sounds.GameSoundPlay();

            if (Player.t != null) {
            Player.t.Start();//temp
           Player.t2.Start(); }
        }

        public void AnimateImage()
        {//Begin the animation only once.
            if (!currentlyAnimating)
            {
                ImageAnimator.Animate(animatedImage, new EventHandler(this.OnFrameChanged));
                currentlyAnimating = true;
            }
        }

        private void OnFrameChanged(object o, EventArgs e)
        {

        }

        private void Game_MouseClick(object sender, MouseEventArgs e)
        {
            menu.Click(e);
        }

        private void start()
        {
            init();
            ticker = new System.Windows.Forms.Timer();
            ticker.Interval = 10;
            ticker.Tick += Ticker_Tick;

            ticker.Start();
            Image img2 = Image.FromFile(@"resources/level.png");
            h.LoadImageLevel((Bitmap)img2);
        }

        private void init()
        {
            WIDTH = this.Width;
            HEIGHT = this.Height;
            menu = new Menu();
            tex = new Texture();
            cam = new Camera(0, 0);
           
            h = createHandler();
        }

        public Handler createHandler()
        {
            return new Handler(this, cam);
        }

        public void stop()
        {
            ticker.Stop();
        }

        private void Ticker_Tick(object sender, EventArgs e)
        {
            if (gameState == STATE.Game)
            {
                update();
                h.tick();
                // WRITES FPS IN CONSOLE
                // Console.WriteLine(Utility.CalculateFrameRate());
            }

            /*  else if (gameState == STATE.Menu)
            {
                if (LobbyHidden)////////////
                    unhideLobby();
            }*/

            this.Invalidate();
        }

        private void update()
        {
                for (int i = 0; i < Handler.gameObject.Count; i++)
                {
                    if (Handler.gameObject.ElementAt(i).getId() == ObjectID.Player)
                    {
                        cam.tick(Handler.gameObject.ElementAt(i));
                        p = (Player)Handler.gameObject.ElementAt(i);
                    }
                }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (gameState == STATE.Menu)
            {
                menu.render(e.Graphics);
            }

            else if (gameState == STATE.Game)
            {
                AnimateImage();
                ImageAnimator.UpdateFrames();
                e.Graphics.DrawImage(this.animatedImage, new Point(0, 0));
                e.Graphics.TranslateTransform(cam.X, cam.Y);
                
                h.render(e.Graphics);

                e.Graphics.TranslateTransform(-cam.X, -cam.Y); // Ends shooting here
            }
        }
    }
}