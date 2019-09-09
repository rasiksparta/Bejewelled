using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jewellery
{
    public class PurpleJewel : Jewel
    {
        public PurpleJewel(int x, int y, JewelSwitcher switcher) : base(x, y, switcher)
        {
            type = Type.PURPLE;
        }

        protected override void SetJewelImage()
        {

        }
    }
}
