using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media.Animation;
using System.Threading;


namespace Jewellery
{
    public abstract class Jewel : Button
    {
        protected int x, y; // Grid index
        protected JewelSwitcher switcher; // the JewelSwitcher
        protected Type type; // the type of this jewel
         
        private JewelManager manager; // the JewelManager

        /**
         * The Type enum, the three existing types of jewel
         */
        public enum Type
        {
            BLUE, RED, PURPLE
        }

        /**
         * The Direction enum, holds the 4 possible direction a valid neighbouring jewel can occur at
         */
        public enum Direction
        {
            LEFT, RIGHT, UP, DOWN, OMNI
        }

        /**
         * Constructor
         * @param x, the x index 
         * @param y, the y index
         * @param switcher, the JewelSwitcher 
         * @param manager, the JewelManager
         */
        public Jewel(int x, int y, JewelSwitcher switcher, JewelManager manager)
        {
            this.x = x;
            this.y = y;
            this.switcher = switcher;
            this.manager = manager;
            this.Click += ButtonClick;
        }

        /**
         * Getters and setters
         */
        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
        public Type JewelType { get => type; }
            
        /**
         * Get opposite direction to a given direction
         * @param direction, the given direction
         * @returns, the opposite direction 
         */
        public static Direction GetOppositeDirection(Direction direction)
        {
            if (direction == Direction.LEFT) return Direction.RIGHT;
            else if (direction == Direction.RIGHT) return Direction.LEFT;
            else if (direction == Direction.UP) return Direction.DOWN;
            else return Direction.UP;
        }
        
        /**
         * Switch place with a given jewel
         * @param jewel, the jewel to exchange place with
         */
        public void SwitchPlace(Jewel jewel)
        {
          //  string str = "before jewelA: " + x + "," + y;
          //  str += " jewelB: " + jewel.x + "," + jewel.y; 
            int prevX = x;
            int prevY = y;

            this.x = jewel.X;
            this.y = jewel.Y;

            jewel.X = prevX;
            jewel.Y = prevY;

            manager.JewelArr[jewel.X, jewel.Y] = jewel;
            manager.JewelArr[x, y] = this;
           // str += " After jewelA: " + manager.JewelArr[x, y].X + "," + manager.JewelArr[x, y].Y;
           // str += " jewelB: " + manager.JewelArr[jewel.X, jewel.Y].X + "," + manager.JewelArr[jewel.X, jewel.Y].Y;
           // MessageBoxResult ms = MessageBox.Show(str);
        }

        /**
         * Find neighbouring jewels of similar type and store them in a Dictionary object
         * with the direction where they are located with respect to this jewel as the key 
         * The direction can be LEFT, RIGHT, UP and DOWN
         * 
         * @returns, the Dictionary object that holds the neighbouring jewels of same type as this
         */
        public Dictionary<Direction, Jewel> GetMachingNeighbour()
        {
            Dictionary<Direction, Jewel> matchingNeighbour = new Dictionary<Direction, Jewel>();
            if (this.x > 0)
            {
                Jewel jewelLeft = manager.JewelArr[this.x - 1, this.y];
                if (jewelLeft != null)
                {
                    if (jewelLeft.JewelType == this.type)
                    {
                        matchingNeighbour.Add(Direction.LEFT, jewelLeft);
                    }
                }
                
            }
            if(this.x < manager.JewelArr.GetLength(0) - 1)
            {
                Jewel jewelRight = manager.JewelArr[this.x + 1, this.y];
                if (jewelRight != null)
                {
                    if (jewelRight.JewelType == this.type)
                    {
                        matchingNeighbour.Add(Direction.RIGHT, jewelRight);
                    }
                }
                
            }
            if(this.y > 0)
            {
                Jewel jewelUp = manager.JewelArr[this.x, this.y - 1];
                if (jewelUp != null)
                {
                    if (jewelUp.JewelType == this.type)
                    {
                        matchingNeighbour.Add(Direction.UP, jewelUp);
                    }
                }
                    
            }
            if(this.y < manager.JewelArr.GetLength(1) - 1)
            {
                Jewel jewelDown = manager.JewelArr[this.x, this.y + 1];
                if (jewelDown != null)
                {
                    if (jewelDown.JewelType == this.type)
                    {
                        matchingNeighbour.Add(Direction.DOWN, jewelDown);
                    }

                }
            }
            return matchingNeighbour;
        }

        /**
         * Drop the jewel by one grid index vertically
         */
        public void JewelDrop()
        {
            if (this.Y < manager.JewelArr.GetLength(0) - 1)
            {
                this.Y += 1;
            }          
        }

        /**
         * Destroy the jewel 
         * Plays fading animation 
         * @param isLast
         */
        public void Destroy(bool isLast)
        {
           
            DoubleAnimation animation = new DoubleAnimation(0, TimeSpan.FromSeconds(1));
            
            // set Event handler
            animation.Completed += (s, e) => {
                manager.AddToAlteredList(this);
                
                //if (isLast)
                //{
                    manager.RefillJewels();
                //}
            };
            BeginAnimation(OpacityProperty, animation);
        }

        /**
         * Tell JewelManager to replace this jewel on Destroy animation completion
         */
        private void OnAnimationComplete()
        {
           
            //manager.ReplaceJewel(this);
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
            if (manager.Container.PlayerWait)
            {
                return;
            }
            //MessageBoxResult box = MessageBox.Show("A jewel is clicked", "My App", MessageBoxButton.YesNoCancel);
            if (switcher.IsFree())
            {
                switcher.Jewel = this;
            }
            else
            {
                Jewel otherJewel = switcher.Jewel;
                if (switcher.Switch(this))
                {
                    manager.Container.PlayerWait = true;
                    manager.Container.ScoreTracker.moveDecrement();
                    HashSet<Jewel> twoJewels = new HashSet<Jewel>() { this, otherJewel};
                    manager.RemoveJewels(twoJewels);    
                    
                } 
            }
        }
    }
}
