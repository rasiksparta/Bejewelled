using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace Jewellery
{
    
    public class Container
    {
        private JewelManager manager; // JewelManager object
        private JewelSwitcher switcher; // JewelSwitcher object
        private ScoreTracker scoreTracker; // ScoreTracker object
        private bool playerWait; // flag for player to wait
        Window win;

        /**
         * Constructor
         * @param win, MainWindow (WPF window object) 
         * @param grid, the game grid element 
         * @param moveCount, the TextBlock for move count
         * @param scoreCount, the TextBlock for score count
         * @param startMove, the number of moves the player starts with
         */
        public Container(Window win, Grid grid, TextBlock scoreCount, TextBlock moveCount, int startMove)
        {
            switcher = new JewelSwitcher();
            manager = new JewelManager(8, 8, switcher, grid, this);
            scoreTracker = new ScoreTracker(moveCount, scoreCount);
            moveCount.Text = startMove.ToString();
            playerWait = false;
            this.win = win;
        }

        /**
         * Getter ans setters for instant variables
         */
        public JewelManager Manager { get => manager; set => manager = value; }
        public JewelSwitcher Switcher { get => switcher; set => switcher = value; }
        public ScoreTracker ScoreTracker { get => scoreTracker; set => scoreTracker = value; }
        public bool PlayerWait { get => playerWait; set => playerWait = value; }

        /**
         * Initiate the game
         */
        public void init()
        {
            manager.Generate();
        }

        /**
         * End the game
         */
        public void End()
        {
            win.DataContext = new FinalViewModel();
            foreach (Jewel jewel in manager.JewelArr)
            {
                jewel.Visibility = Visibility.Hidden;
            }           
        }
    }
}
