using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameForm.window
{
    /// <summary>
    /// Animates sprites in a given array assuming the sprites are in a sprite sheet
    /// </summary>
    public class Animation
    {
        private int speed;
        private int frames;

        private int index = 0;
        private int count = 0;

        private Bitmap[] images;
        private Bitmap currentImg;


        public Animation(int speed, params Bitmap[] values)
        {
            this.speed = speed;
            images = new Bitmap[values.Length];

            for (int i = 0; i < values.Length; i++)
            {
                images[i] = values[i];
            }
            frames = values.Length;
        }

        public void runAnimation()
        {
            index++;
            if (index >= speed)
            {
                index = 0;
                nextFrame();
            }
        }
        private void nextFrame()
        {
            for (int i = 0; i < frames; i++)
            {
                if (count == i)  //setting current image
                    currentImg = images[i];
            }
            count++;

            if (count > frames)
                count = 0;
        }

        public void drawAnimation(Graphics g, int x, int y)
        {
            g.DrawImage(currentImg, (int)x, (int)y);
        }
        public void drawAnimation(Graphics g, int x, int y, int scaleX, int scaleY)
        {
            g.DrawImage(currentImg, (int)x, (int)y, scaleX, scaleY);
        }

    }
}

