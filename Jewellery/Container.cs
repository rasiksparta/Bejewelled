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
        private JewelManager manager;
        private JewelSwitcher switcher;
        private ScoreTracker scoreTracker;
        private bool playerWait;
        Window win;

        public Container(Window win, Grid grid, TextBlock scoreCount, TextBlock moveCount, int startMove)
        {
            switcher = new JewelSwitcher(grid);
            manager = new JewelManager(8, 8, switcher, grid, this);
            scoreTracker = new ScoreTracker(moveCount, scoreCount);
            moveCount.Text = startMove.ToString();
            playerWait = false;
            this.win = win;
        }

        public JewelManager Manager { get => manager; set => manager = value; }
        public JewelSwitcher Switcher { get => switcher; set => switcher = value; }
        public ScoreTracker ScoreTracker { get => scoreTracker; set => scoreTracker = value; }
        public bool PlayerWait { get => playerWait; set => playerWait = value; }

        public void init()
        {
            manager.Generate();
        }

        public void End()
        {
            win.DataContext = new FinalViewModel();
            foreach (Jewel jewel in manager.JewelArr)
            {
                jewel.Visibility = Visibility.Hidden;
            }
            //finalMessage.InitializeComponent();
            //MessageBoxResult ms = MessageBox.Show($"Game over, your score is { scoreTracker.getScore() }");
        }
    }
}
