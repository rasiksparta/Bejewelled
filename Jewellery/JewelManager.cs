using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media.Animation;

namespace Jewellery
{
    public class JewelManager
    {
        Jewel[,] jewelArr; // 2D array that stores all the jewels, index corresponds to the jewel's ingame grid index
        JewelSwitcher switcher; // JeweSwitcher object for switching jewels
        Random rand;  // Random number generator
        private Grid grid; // The game grid 
        private int colNum, rowNum; 

        bool addJewel;
        private HashSet<Jewel> alteredJewels; // list of jewels that have been added as replacement or jewel whose location has been changed
                                              // since last move
        private Container container;
        private int jewelDeletionCount;

        /**
         * Constructor
         * @param rowNum, the number of rows in the grid
         * @param colNum, the number of columns in the grid
         * @param switcher, jewel switcher object 
         * @param grid, WPF element grid for the game
         */
        public JewelManager(int rowNum, int colNum, JewelSwitcher switcher, Grid grid, Container container)
        {
            jewelArr = new Jewel[rowNum, colNum];
            rand = new Random();
            this.switcher = switcher;
            this.grid = grid;
            this.colNum = colNum;
            this.rowNum = rowNum;
            addJewel = true;
            alteredJewels = new HashSet<Jewel>();
            this.container = container;
            jewelDeletionCount = 0;
        }

        public Jewel[,] JewelArr { get => jewelArr; }
        public Container Container { get => container; set => container = value; }

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
                    bool goodToPlace = false; // True if the jewel is fit to be placed
                    Jewel jewel = null;
                    while (!goodToPlace)
                    {
                        double x = rand.Next(3);
                        int foo = (int)Math.Floor(x);
                        jewel = CreateJewel(foo, col, row);

                        //Avoid creating any chains in the starting configuration
                        if(CheckForChain(0, jewel, new HashSet<Jewel>()) < 3)
                        {
                            goodToPlace = true;
                        }
                    }
                   
                    jewelArr[col, row] = jewel;
                }
            }
            Update(jewelArr);
        }

        /**
         * Update add jewels  
         * @param jewelArr, the array of jewels to add
         */
        public void Update(Jewel[,] jewelArr)
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
        private Jewel CreateJewel(int type, int x, int y)
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

        /**
         * Remove a list of jewels
         * @param jewels, the list of jewels to be removed
         */
        public void RemoveJewels(HashSet<Jewel> jewels)
        {
            //MessageBoxResult ms = MessageBox.Show("jewels to remove: " + jewels.Count);
            bool isLast = false; // check for last element
            HashSet<Jewel> removeList = new HashSet<Jewel>();

            // Get all jewels in valid chains 
            foreach (Jewel jewel in jewels)
            {
                HashSet<Jewel> temp = new HashSet<Jewel>();
                CheckForChain(0, jewel, temp);
                removeList = new HashSet<Jewel>(removeList.Concat(temp));
            }

            container.ScoreTracker.ScoreIncrement(removeList.Count);

            jewelDeletionCount = removeList.Count;
            // remove the jewels
            foreach (Jewel jewelToRemove in removeList)
            {
                //jewelArr[jewelToRemove.X, jewelToRemove.Y] = null;
                if (removeList.Last().Equals(jewelToRemove))
                {
                    isLast = true;
                    //ms = MessageBox.Show("checkpoint");
                }
                jewelToRemove.Destroy(isLast);
            }

            if (removeList.Count == 0)
            {
                //ms = MessageBox.Show("remove list is empty");
                //MessageBoxResult ms = MessageBox.Show(alteredJewels.Count.ToString());
                if (container.ScoreTracker.getMoveCount() <= 0)
                {
                    container.End();
                }
                container.PlayerWait = false;
            }
        }

        /**
         * Refill grid with jewels after jewels are removed from the grid
         */
        public void RefillJewels()
        {
            if (jewelDeletionCount > 0)
            {
                return;
            }
            //HashSet<Jewel> newJewels = new HashSet<Jewel>();
            HashSet<Jewel> newJewels = new HashSet<Jewel>();
            foreach(Jewel jewel in alteredJewels)
            {
                int type = (int)rand.Next(3);
                Jewel newJewel = CreateJewel(type, jewel.X, jewel.Y);
                newJewels.Add(newJewel);
                jewelArr[newJewel.X, newJewel.Y] = newJewel;
              //  MessageBoxResult m = MessageBox.Show(newJewel.X.ToString()); ;

                grid.Children.Remove(jewel);
                grid.Children.Add(newJewel);
                Grid.SetColumn(newJewel, newJewel.X);
                Grid.SetRow(newJewel, newJewel.Y);
            }
            //MessageBoxResult ms = MessageBox.Show("new jewel: " + alteredJewels.Count.ToString());

            alteredJewels.Clear();
            //ms = MessageBox.Show("new jewel added");
            RemoveJewels(newJewels);
        }

        /**
         * Add a jewel to the list of altered jewel
         * Note: An altered jewel is a jewel which has been added as replacement or has changed its location
         */
        public void AddToAlteredList(Jewel jewel)
        {
            jewelDeletionCount--;
            alteredJewels.Add(jewel);
        }

        /**
         * Check if a chain of jewels of same type exists either horizontally or vertically
         * @param count, the number of jewels in the chain
         * @param jewel, the starting jewel 
         * @removeList, the list of all jewels in the chain of 3 which will be removed 
         * @direction, the direction of search, LEFT, RIGHT, UP or DOWN
         * @root, the previous jewel in chain
         * 
         * @return, the count of jewels in chain
         */
        private int CheckForChain(int count, Jewel jewel, HashSet<Jewel> removeList ,Jewel.Direction direction = Jewel.Direction.LEFT ,Jewel root = null)
        {
            if (removeList.Contains(jewel)) return 0;
            count++;
            // if root is null, which means this is the first jewel from where we start checking for a chain
            if(root == null) {
                // check if there are any neighbouring jewel of same type
                Dictionary<Jewel.Direction,Jewel> branches = jewel.GetMachingNeighbour();              

                foreach (Jewel.Direction dir in branches.Keys)
                {
                    Jewel branch;
                    branches.TryGetValue(dir, out branch);
                    count = CheckForChain(count, branch, removeList, dir, jewel);
                    branch = null;
                    branches.TryGetValue(Jewel.GetOppositeDirection(dir), out branch);
                    if(branch != null)
                    {
                        count = CheckForChain(count, branch, removeList, Jewel.GetOppositeDirection(dir), jewel);
                    }

                    if (count >= 3)
                    {
                        removeList.Add(jewel);
                        return count;
                    }
                    else
                    {
                        removeList.Clear();
                        count = 1;
                    }
                }
            }
            else
            {
                Dictionary<Jewel.Direction, Jewel> branches = jewel.GetMachingNeighbour();

                // Keep checking for chain in the same direction as this jewel was reached from its root element
                if (branches.Keys.Contains(direction))
                {
                    Jewel branch;
                    branches.TryGetValue(direction, out branch);
                    count = CheckForChain(count, branch, removeList, direction, jewel);
                }
                removeList.Add(jewel);
            }
            return count;
        }
    }
}
