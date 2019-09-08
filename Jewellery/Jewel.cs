using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;


namespace Jewellery
{
    public abstract class Jewel : Button
    {
        protected int x, y;
        public Jewel(int x, int y)
        {
            this.x = x;
            this.y = y;
            SetJewelImage();
        }

        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }

        public virtual void SetJewelImage()
        {
            this.DefaultStyleKey = typeof(Jewel);

        }
    }
}
