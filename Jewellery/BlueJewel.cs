using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jewellery
{
    public class BlueJewel : Jewel
    {
        public BlueJewel(int x, int y, JewelSwitcher switcher) : base(x, y, switcher)
        {
            type = Type.BLUE;
        }

        protected override void SetJewelImage()
        {

        }
    }
}
