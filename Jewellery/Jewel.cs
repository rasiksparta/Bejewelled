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

        public enum Type
        {
            BLUE, RED, PURPLE
        }

        public Jewel(int x, int y, JewelSwitcher switcher)
        {
            this.x = x;
            this.y = y;
            this.switcher = switcher;
            this.Click += ButtonClick;
            //SetJewelImage();
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
            }
        }
    }
}
