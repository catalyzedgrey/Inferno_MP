using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace GameForm.framework
{
    public class SpriteSheet
    {

        private Bitmap image;
        public SpriteSheet(Bitmap image)
        {
            this.image = image;
        }

        public Bitmap Load(int col, int row, int w, int h, string sprite)
        {
            Image orig = Image.FromFile(@"resources/"+ sprite);
            
            Rectangle rectangle = new Rectangle((col * w) - w, (row * h) - h, w, h);
            Bitmap bit = (Bitmap)orig;

            //copy certain pictures fromt he sprite sheet (ex: the first walking frame of animation)
            return (Bitmap)bit.Clone(rectangle, bit.PixelFormat);
            //return (Bitmap)original.Clone(rectangle, original.PixelFormat);

         //   if (orig != null) // unreachable
            //    orig.Dispose();
        }

    }
}
