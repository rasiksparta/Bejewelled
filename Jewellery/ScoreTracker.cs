﻿using System;
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

        public ScoreTracker(TextBlock moveCount, TextBlock scoreCount)
        {
            this.moveCount = moveCount;
            this.scoreCount = scoreCount;
        }

        public void ScoreIncrement(int value)
        {
            int score = Int32.Parse(scoreCount.Text);
            score += value * value;
            scoreCount.Text = score.ToString();
        }


        public void moveDecrement()
        {
            int move = Int32.Parse(moveCount.Text);
            move--;
            moveCount.Text = move.ToString();
        }

        public int getMoveCount()
        {
            return Int32.Parse(moveCount.Text);
        }
        public int getScore()
        {
            return Int32.Parse(scoreCount.Text);
        }
    }
}
