using GameForm.objects;

namespace GameForm.window
{
    public class Camera
    {
        private float x, y;
        private int level = 1;

        public float X
        {
            get { return this.x; }
            set { this.x = value; }
        }

        public float Y
        {
            get { return this.y; }
            set { this.y = value; }
        }
        public Camera(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public void tick(GameObject player)
        {
            if (level == 1)
            {

                  if (player.X > 300) {
                    //    Console.WriteLine(" fe eh");
                         if (x > -300)
                            x -= 2;
                    }
                    else if(player.X < 300)
                    {
                        if (x < 0)
                            x+=2;
                    }

                //   x = -player.X + Game.WIDTH*2 ;
               // x = x - 1f;
                // HUD.velX = (int)player.getX();
            }

        }
    }
}