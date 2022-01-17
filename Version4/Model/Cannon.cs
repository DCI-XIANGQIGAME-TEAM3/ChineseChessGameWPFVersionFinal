using System;
using Model;


namespace Control
{
    class Cannon : Chess
    {
        public override bool ChessMovingRule(int CurrentX, int CurrentY, int OriginalX, int OriginalY, Chess[,] Matrix)
        {
            int min, max, k, num=0;
            // Can't walk diagonally(X, Y coordinates cannot be changed at the same time)
            if (CurrentX != OriginalX && CurrentY != OriginalY)
            {
                return false;
            }

            if (OriginalX == CurrentX) // move Y coordinate
            {
                if (OriginalY < CurrentY)
                {
                    // If the OriginalY is less than the CurrentY, min is equal to the OriginalY
                    min = OriginalY;
                    max = CurrentY;
                }
                else
                {
                    // If the OriginalY is greater than the CurrentY, min is equal to the CurrentY
                    min = CurrentY;
                    max = OriginalY;
                 
                }
                num = 0;
                // Number of chess between CurrentY and OriginalY
                for (k = min + 1; k < max; k++)
                {
                    if (Matrix[CurrentX, k].side != Chess.Player.blank)
                    {
                        num++;
                    }
                }
            }
            else if (OriginalY == CurrentY) // move X coordinate
            {
                if (OriginalX < CurrentX)
                {
                    min = OriginalX;
                    max = CurrentX;
                }
                else
                {
                    min = CurrentX;
                    max = OriginalX;
                }

                num = 0;

                for (k = min + 1; k < max; k++)
                {
                    if (Matrix[k, CurrentY].side != Chess.Player.blank)
                    {
                        num++;
                    }
                }
            }

            // If the number of chess in the middle is more than one, it means that it cannot be moved
            if (num > 1)
            {
                return false;
            }
            // It can't moved if CurrentX and CurrentY have chess
            if (num == 0 && Matrix[CurrentX, CurrentY].side != Chess.Player.blank)
            {
                return false;
            }
            // There is a chess in the middle, but the CurrentX and CurrentY are empty
            if (num == 1 && Matrix[CurrentX, CurrentY].side == Chess.Player.blank)
            {
                return false;
            }
            // Cannot capture chess from the same side
            if (Matrix[OriginalX, OriginalY].side == Matrix[CurrentX, CurrentY].side)
            {
                return false;
            }

            ProgramControl.SetMove(CurrentX, CurrentY, OriginalX, OriginalY, Matrix);

            return true;
        }

    }
}
