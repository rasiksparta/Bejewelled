using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Jewellery;

namespace Bejewelled
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public const int ROW = 8;
        public const int COLUMN = 8;

        JewelSwitcher switcher;
        JewelManager manager;

        Grid grid;
        Random rand;
        public MainWindow()
        {
            InitializeComponent();
            rand = new Random();
            grid = gamegrid;
            switcher = new JewelSwitcher(grid);
            manager = new JewelManager(ROW, COLUMN, switcher, grid);
            manager.Generate();
            manager.Update();
        }

        public void Update()
        {
            Jewel[,] jewelArray = manager.JewelArr;

            foreach(Jewel jewel in jewelArray)
            {
                if (jewel != null)
                {
                    grid.Children.Add(jewel);
                    Grid.SetColumn(jewel, jewel.X);
                    Grid.SetRow(jewel, jewel.Y);
                }
            }
        }

    }
}
