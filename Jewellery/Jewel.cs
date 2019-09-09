using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;


namespace Jewellery
{
    public abstract class Jewel : Button
    {
        protected int x, y;
        protected JewelSwitcher switcher;
        protected Type type;

        private JewelManager manager;

        public enum Type
        {
            BLUE, RED, PURPLE
        }

        public Jewel(int x, int y, JewelSwitcher switcher, JewelManager manager)
        {
            this.x = x;
            this.y = y;
            this.switcher = switcher;
            this.manager = manager;
            this.Click += ButtonClick;
        }

        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
        public Type JewelType { get => type; }

        public void SwitchPlace(Jewel jewel)
        {
            int prevX = x;
            int prevY = y;

            this.x = jewel.X;
            this.y = jewel.Y;

            jewel.X = prevX;
            jewel.Y = prevY;

            manager.JewelArr[jewel.X, jewel.Y] = jewel;
            manager.JewelArr[this.x, this.y] = this;
        }

        public List<Jewel> GetMachingNeighbour()
        {
            List<Jewel> matchineNeighbour = new List<Jewel>();
            Jewel jewelLeft = manager.JewelArr[this.x - 1, this.y];
            Jewel jewelRight = manager.JewelArr[this.x + 1, this.y];
            Jewel jewelUp = manager.JewelArr[this.x, this.y - 1];
            Jewel jewelDown = manager.JewelArr[this.x, this.y + 1];

            if (jewelLeft.JewelType == this.type) matchineNeighbour.Add(jewelLeft);
            if (jewelRight.JewelType == this.type) matchineNeighbour.Add(jewelRight);
            if (jewelUp.JewelType == this.type) matchineNeighbour.Add(jewelUp);
            if (jewelDown.JewelType == this.type) matchineNeighbour.Add(jewelDown);

            return matchineNeighbour;
        }

        protected virtual void SetJewelImage()
        {
            //this.DefaultStyleKey = typeof(Jewel);
        }

        /**
         * Action on being clicked on
         * @param sender, the button object 
         * @param RoutedEventArgs e, related click properties 
         */
        protected virtual void ButtonClick(object sender, RoutedEventArgs e)
        {

            //MessageBoxResult box = MessageBox.Show("A jewel is clicked", "My App", MessageBoxButton.YesNoCancel);
            if (switcher.IsFree())
            {
                switcher.Jewel = this;
            }
            else
            {
                switcher.Switch(this);
                manager.removeJewels(this);
               // manager.removeJewels(switcher.Jewel);
            }
        }
    }
}
