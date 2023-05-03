using System.Drawing;
using System.Windows.Media.Imaging;

namespace Pix2TexWpf
{
    public class CapturedImageEventArgs
    {
        public Bitmap Image { get; set; }
        
        public CapturedImageEventArgs(Bitmap image)
        {
            this.Image = image;
        }
    }
}