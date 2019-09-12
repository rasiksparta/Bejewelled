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
        private Jewel jewel; // stores initial jewel which is then swapped with the next neighbouring jewel that is clicked

        /**
         * Jewel switcher class         
         */
        public JewelSwitcher()
        {
            this.jewel = null;            
        }


        /**
         * Set jewel
         */
        public Jewel Jewel { get => jewel;  set => jewel = value; }


        /**
         * Switch a given jewel with the initially stored jewel if the switch is valid
         * @param jewel, the jewel to be switched with the innitial jewel       
         */
        public bool Switch(Jewel jewel){
            if(this.jewel != null)
            {
                if (this.jewel == jewel)
                {
                    return false;
                }

                if (!CheckIfNeighbours(this.jewel, jewel))
                {
                    this.jewel = jewel;
                    return false;
                }
                this.jewel.SwitchPlace(jewel);

                Grid.SetColumn(jewel, jewel.X);
                Grid.SetRow(jewel, jewel.Y);
                Grid.SetColumn(this.jewel, this.jewel.X);
                Grid.SetRow(this.jewel, this.jewel.Y);

                this.jewel = null;
                return true;
            }
            return false;
        }

        /**
         * Check if the switcher is holding any innitial jewel
         * @return, true if switcher is not holding any innitial jewel 
         */
        public bool IsFree()
        {
            if (this.jewel == null)
            {
                return true;
            }

            return false;
        }

        /**
         * Checks if two jewels are neighbouring jewels
         * Jewels that are diagonal to each other are not considered neighbour
         * @param jewelA, first jewel 
         * @param jewelB, second jewel
         */
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
