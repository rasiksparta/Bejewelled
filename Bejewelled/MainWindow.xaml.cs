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
            manager = new JewelManager(ROW, COLUMN, switcher);
            Update();
        }

        public void Update()
        {
            Jewel[,] jewelArray = manager.Generate();

            foreach(Jewel jewel in jewelArray)
            {
                grid.Children.Add(jewel);
                Grid.SetColumn(jewel, jewel.X);
                Grid.SetRow(jewel, jewel.Y);
            }
            /*for (int col = 0; col < COLUMN; col++)
            {
                for(int row = 0; row < ROW; row++)
                {
                    double x = rand.Next(3);
                    int foo = (int)Math.Floor(x);
                    Jewel jewel = CreateJewel(foo, col, row);                    
                    grid.Children.Add(jewel);
                    Grid.SetColumn(jewel, jewel.X);
                    Grid.SetRow(jewel, jewel.Y);
                }
            }*/
        }

        public Jewel CreateJewel(int type, int x, int y)
        {
            switch (type)
            {
                case 0:
                    return new RedJewel(x, y, switcher);
                case 1:
                    return new PurpleJewel(x, y, switcher);
                default:
                    return new BlueJewel(x, y, switcher);
            }
        }

    }
}
