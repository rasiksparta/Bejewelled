using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace Jewellery
{
    public class JewelManager
    {
        Jewel[,] jewelArr;
        JewelSwitcher switcher;
        Random rand;
        private Grid grid;
        private int colNum, rowNum;

        bool addJewel;

        public Jewel[,] JewelArr { get => jewelArr; }

        public JewelManager(int rowNum, int colNum, JewelSwitcher switcher, Grid grid)
        {
            jewelArr = new Jewel[rowNum, colNum];
            rand = new Random();
            this.switcher = switcher;
            this.grid = grid;
            this.colNum = colNum;
            this.rowNum = rowNum;
            addJewel = true;
        }

        /**
         * Generate the 2D array of jewel before initiating the game
         * @return the array of jewel
         */
        public void Generate()
        {
            for (int row = 0; row < jewelArr.GetLength(0); row++)
            {
                for(int col = 0; col < jewelArr.GetLength(1); col++)
                {
                    double x = rand.Next(3);
                    int foo = (int)Math.Floor(x);
                    Jewel jewel = CreateJewel(foo, col, row);
                    jewelArr[col, row] = jewel;
                }
            }
        }

        public void Update()
        {
            if (addJewel)
            {
                addJewel = false;
                foreach(Jewel jewel in jewelArr)
                {
                    grid.Children.Add(jewel);
                    Grid.SetColumn(jewel, jewel.X);
                    Grid.SetRow(jewel, jewel.Y);
                }
            }            
        }

        /**
         * Creates a jewel object depending on the int type
         * @param type, the type of jewel 0 - RedJewel 1 - PurpleJewel 2 - BlueJewel
         * @param int x, y, the grid position of the object
         * @return the jewel object
         */
        public Jewel CreateJewel(int type, int x, int y)
        {
            switch (type)
            {
                case 0:
                    return new RedJewel(x, y, switcher, this);
                case 1:
                    return new PurpleJewel(x, y, switcher, this);
                default:
                    return new BlueJewel(x, y, switcher, this);
            }
        }

        public void removeJewels(Jewel jewel)
        {
           // List<Jewel> removelist = new List<Jewel>();
            bool shouldRemove = false;
            if (CheckForChain(jewel, 1, true, 1) >= 3) shouldRemove = true;
            if (CheckForChain(jewel, 1, true, -1) >= 3) shouldRemove = true;
            if (CheckForChain(jewel, 1, false, 1) >= 3) shouldRemove = true;
            if (CheckForChain(jewel, 1, false, -1) >= 3) shouldRemove = true;
            if (shouldRemove) jewel.Visibility = Visibility.Hidden;
            // remove jewels
        }

        private int CheckForChain(Jewel jewel, int count, bool recurseHorizontal, int dir)
        {
            //MessageBoxResult ms = MessageBox.Show("recursing" +" count:" + count + ", direction:" + dir + ", type:" + jewel.JewelType 
            //    + " coordinates:" + jewel.X);
            int recurseStep = count;

            if((dir == 1 && jewel.X >= colNum - 1) || (dir == -1 && jewel.X <= 0))
            {
                if (count >= 3) jewel.Visibility = Visibility.Hidden;
                return count;
            }
            Jewel next = recurseHorizontal? jewelArr[jewel.X + dir, jewel.Y] : jewelArr[jewel.X, jewel.Y + dir];
            
            if (next.JewelType == jewel.JewelType)
            {
                count++;          
                count = CheckForChain(next, count, true, dir);
            }
         
            if(count >= 3 && recurseStep != 1)
            {
                jewel.Visibility = Visibility.Hidden;
            }
        

            return count;
        }

        private int CheckForChain2(Jewel jewel)
        {

            return 0;
        }
    }
}
