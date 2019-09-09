using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jewellery
{
    public class JewelManager
    {
        Jewel[,] jewelArr;
        JewelSwitcher switcher;
        Random rand;
        public JewelManager(int rowNum, int colNum, JewelSwitcher switcher)
        {
            jewelArr = new Jewel[rowNum, colNum];
            rand = new Random();
            this.switcher = switcher;
        }

        /**
         * Generate the 2D array of jewel before initiating the game
         * @return the array of jewel
         */
        public Jewel[,] Generate()
        {
            for (int row = 0; row < jewelArr.GetLength(0); row++)
            {
                for(int col = 0; col < jewelArr.GetLength(1); col++)
                {
                    double x = rand.Next(3);
                    int foo = (int)Math.Floor(x);
                    Jewel jewel = CreateJewel(foo, col, row);
                    jewelArr[row, col] = jewel;
                }
            }

            return jewelArr;
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
                    return new RedJewel(x, y, switcher);
                case 1:
                    return new PurpleJewel(x, y, switcher);
                default:
                    return new BlueJewel(x, y, switcher);
            }
        }

        public void checkRowForChain()
        {
            List<Jewel> removeArr = new List<Jewel>();
            Jewel chainElementA = null;
            Jewel chainElementB = null;
            Jewel chainElementC = null;

            for (int row = 0; row < 10; row++)
            {
                for(int col = 0; col < 20; col++)
                {
                    List<Jewel> rowRemove = new List<Jewel>();
                    if(chainElementA == null)
                    {
                        chainElementA = jewelArr[row, col];                   
                    }
                    else
                    {
                        Jewel current = jewelArr[row, col];
                        if(chainElementA.JewelType == current.JewelType)
                        {
                            if(chainElementB == null)
                            {
                                chainElementB = current;
                            }else if (chainElementC == null)
                            {
                                chainElementC = current;
                                rowRemove.Add(chainElementA);
                                rowRemove.Add(chainElementB);
                                rowRemove.Add(chainElementC);
                            }
                            else
                            {
                                rowRemove.Add(current); 
                            }
                        }
                        else
                        {
                            chainElementA = null;
                            chainElementB = null;
                            chainElementC = null;

                        }
                    }                    
                }                
            }
        }
    }
}
