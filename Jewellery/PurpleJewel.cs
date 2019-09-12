using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jewellery
{
    public class PurpleJewel : Jewel
    {
        /**
        * Constructor
        * @param x, the x index 
        * @param y, the y index
        * @param switcher, the JewelSwitcher 
        * @param manager, the JewelManager
        */
        public PurpleJewel(int x, int y, JewelSwitcher switcher, JewelManager manager) : base(x, y, switcher, manager)
        {
            type = Type.PURPLE;
        }

        protected override void SetJewelImage()
        {

        }
    }
}
