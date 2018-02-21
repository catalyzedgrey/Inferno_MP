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
    public class Circle
    {
        public PointF pos;
        public PointF center;
        public float r, sideLength;
        public PointF origPos;
        public Circle(float x, float y, float sideL)

        {
            this.pos = new PointF(x, y);
            origPos = new PointF(x, y - 120);
            this.center = new PointF(pos.X + (sideLength / 2), pos.Y + (sideLength / 2));
            this.sideLength = sideL;
            this.r = sideL / 2;
        }

        public Circle(PointF p, float sideL)

        {
            this.pos = p;
            this.center = new PointF(pos.X + (sideLength / 2), pos.Y + (sideLength / 2));
            this.sideLength = sideL;
            this.r = sideL / 2;
        }

        public bool IntersectsWith(Rectangle r, bool player)
        {
            double ex =
                    Math.Sqrt(
                     Math.Pow(Math.Abs((int)(center.X - r.X)), 2) +
                     Math.Pow(Math.Abs((int)(center.Y - r.Y)), 2)
                        );

            if (player)
            {
                if (ex < this.r + 30)
                    return true;
            }
            else
            {
                if (ex < this.r)
                    return true;
            }

            return false;
        }
    }

    public class Boulder : GameObject
    {
        Circle c;
        public Circle C { get { return c; } }


        Bitmap animatedImage = new Bitmap(@"resources\boulder.gif");
        bool currentlyAnimating = false;

        public Font Arial { get; private set; }

        public Boulder(float x, float y, ObjectID id)
            : base(x, y, id)
        {
            c = new Circle(x, y, 130);
            c.pos.Y = -200;
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
            //always update new center point
            c.center = new PointF(c.pos.X + (c.sideLength / 2), c.pos.Y + (c.sideLength / 2));
            
            //when falls down, starts from original position
            if (c.pos.X < 0)
                c.pos = c.origPos;

            // bite.runAnimation();

            //move boulder
            c.pos.X -= 4;
            c.pos.Y += 10;// Will cause boulder Vibration bug, when hit the ground

            for (int i = 0; i < Handler.gameObject.Count; i++)
            {
                if (Handler.gameObject.ElementAt(i).getId() == ObjectID.Block)
                {
                    Rectangle r = Handler.gameObject.ElementAt(i).getBounds();

                    if (c.IntersectsWith(r, false))
                    {
                        c.pos.Y -= 10;//go back up 10 pixels

                        //Console.WriteLine("HIT THE FROUND");
                    }

                }
            }
        }

        public override void render(Graphics g)
        {
            //bite.drawAnimation(g, (int)x, (int)y, 192, 79);

            AnimateImage();
            ImageAnimator.UpdateFrames();
            g.DrawImage(this.animatedImage, c.pos);


            #region DRAWINGS TO ILLUSTRATE GAME MECHANICS
            if (debugDrawings)
            {
                g.DrawRectangle(new Pen(Color.Green), c.pos.X, c.pos.Y, c.sideLength, c.sideLength);
                g.DrawEllipse(new Pen(Color.Red, 9), c.pos.X, c.pos.Y, c.sideLength, c.sideLength);

                for (int i = 0; i < Handler.gameObject.Count; i++)
                {
                    if (Handler.gameObject.ElementAt(i).getId() == ObjectID.Player)
                    {
                        Rectangle r = Handler.gameObject.ElementAt(i).getBounds();
                        PointF playerpos = new PointF(r.X + (r.Width / 2), r.Y + (r.Height / 2));
                        //draw line from center of player to center of boulder
                        g.DrawLine(new Pen(Color.Aqua), c.center, playerpos);
                        //draw player pos
                        g.DrawString(playerpos.ToString(), new Font("Arial", 20), new SolidBrush(Color.Red), playerpos);
                        //draw boulder pos
                        g.DrawString(c.center.ToString(), new Font("Arial", 20), new SolidBrush(Color.Red), c.center);
                    }
                }
            }
            #endregion
        }

        public override Rectangle getBounds()
        {
            throw new NotImplementedException();
        }
    }
}
