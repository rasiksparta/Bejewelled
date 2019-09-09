using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;


namespace Jewellery
{
    public class JewelSwitcher
    {
        private Jewel jewel;

        /**
         * Jewel switcher class
         * @jewel, first selected jewel
         */
        public JewelSwitcher(Grid grid)
        {
            this.jewel = null;
        }


        /**
         * Setter
         */
        public Jewel Jewel { set => jewel = value; }


        /**
         * Switch two jewel
         */
        public void Switch(Jewel jewel){
            if(this.jewel != null)
            {
                if (this.jewel == jewel)
                {
                    return;
                }

                if (!CheckIfNeighbours(this.jewel, jewel))
                {
                    this.jewel = jewel;
                    return;
                }
                this.jewel.SwitchPlace(jewel);

                Grid.SetColumn(jewel, jewel.X);
                Grid.SetRow(jewel, jewel.Y);
                Grid.SetColumn(this.jewel, this.jewel.X);
                Grid.SetRow(this.jewel, this.jewel.Y);

                this.jewel = null;
            }
        }

        public bool IsFree()
        {
            if (this.jewel == null)
            {
                return true;
            }

            return false;
        }

        private bool CheckIfNeighbours(Jewel jewelA, Jewel jewelB)
        {
            int difference = Math.Abs(jewelA.X - jewelB.X) + Math.Abs(jewelA.Y - jewelB.Y); 
            if(difference > 1)
            {
                return false;
            }
            return true;
        }
    }
}
