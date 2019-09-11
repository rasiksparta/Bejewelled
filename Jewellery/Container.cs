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

        public Container(Grid grid, TextBlock scoreCount, TextBlock moveCount, int startMove)
        {
            switcher = new JewelSwitcher(grid);
            manager = new JewelManager(8, 8, switcher, grid, this);
            scoreTracker = new ScoreTracker(moveCount, scoreCount);
            moveCount.Text = startMove.ToString();
        }

        public JewelManager Manager { get => manager; set => manager = value; }
        public JewelSwitcher Switcher { get => switcher; set => switcher = value; }
        public ScoreTracker ScoreTracker { get => scoreTracker; set => scoreTracker = value; }

        public void init()
        {
            manager.Generate();
        }

        public void End()
        {
            MessageBoxResult ms = MessageBox.Show($"Game over, your score is { scoreTracker.getScore() }");
        }
    }
}
