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
        public MainWindow()
        {
            InitializeComponent();
            Jewel jewel = new PurpleJewel(0,0);
            Grid grid = gamegrid;
            grid.Children.Add(jewel);
            Grid.SetColumn(jewel, 10);
            Grid.SetRow(jewel, 5);
        }
    }
}
