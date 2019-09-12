using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Jewellery
{
    
    public class ScoreTracker
    {
        private TextBlock moveCount;
        private TextBlock scoreCount;

        /**
         * Constructor 
         * @param moveCount, the TextBlock element that displays move count
         * @param scoreCount, the TextBlock element that displays score count
         */
        public ScoreTracker(TextBlock moveCount, TextBlock scoreCount)
        {
            this.moveCount = moveCount;
            this.scoreCount = scoreCount;
        }

        /**
         * Increment score 
         * @param value, the amount by which to increment the score
         */
        public void ScoreIncrement(int value)
        {
            int score = Int32.Parse(scoreCount.Text);
            score += value * value;
            scoreCount.Text = score.ToString();
        }


        /**
         * Decrement the move count by 1
         */
        public void moveDecrement()
        {
            int move = Int32.Parse(moveCount.Text);
            move--;
            moveCount.Text = move.ToString();
        }

        /**
         * @return move count
         */
        public int getMoveCount()
        {
            return Int32.Parse(moveCount.Text);
        }

        /**
         * @return score count
         */
        public int getScore()
        {
            return Int32.Parse(scoreCount.Text);
        }
    }
}
