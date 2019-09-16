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

        Grid grid;

        /**
         * Constructor        
         */
        public MainWindow()
        {
            InitializeComponent();
            grid = gamegrid;
            Container container = new Container(this ,grid, score, movecount, 100);
            container.init();
        }

    }
}
