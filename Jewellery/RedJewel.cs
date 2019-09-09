using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jewellery
{
    public class RedJewel : Jewel
    {
        public RedJewel(int x, int y, JewelSwitcher switcher, JewelManager manager) : base(x, y, switcher, manager)
        {
            type = Type.RED;
        }

        protected override void SetJewelImage()
        {

        }
    }
}
