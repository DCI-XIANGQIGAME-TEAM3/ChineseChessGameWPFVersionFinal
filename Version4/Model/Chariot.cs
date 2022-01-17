using System;
using Model;

namespace Control
{
    class Chariot : Chess
    {
        public override bool ChessMovingRule(int CurrentX, int CurrentY, int OriginalX, int OriginalY, Chess[,] Matrix)
        {
            int min, max; //use min to record the small coordinate and max to record the big coordinate
            //if the Y coordinate changes
            if (OriginalX == CurrentX)
            {
                if (OriginalY < CurrentY) // move left
                {
                    min = OriginalY;
                    max = CurrentY;
                }
                else //move right
                {
                    min = CurrentY;
                    max = OriginalY;
                }
                //There can be no pieces between the original and current y coordinates
                for (int i = min + 1; i < max; i++)
                {
                    if (Matrix[CurrentX, i].side != Chess.Player.blank)
                    {
                        return false;
                    }
                }

            }
            else if (OriginalY == CurrentY)
            {
                if (OriginalX < CurrentX) //Move down
                {
                    min = OriginalX;
                    max = CurrentX;
                }
                else //Move up
                {
                    min = CurrentX;
                    max = OriginalX;
                }
                //There can be no pieces between the original and current x coordinates
                for (int i = min + 1; i < max; i++)
                {
                    if (Matrix[i, CurrentY].side != Chess.Player.blank)
                    {
                        return false;
                    }
                }

            }
                //can't eat your own chess pieces
                if (Matrix[OriginalX, OriginalY].side == Matrix[CurrentX, CurrentY].side)
            {
                return false;
            }
            //The X and Y coordinates cannot be changed at the same time
            if (OriginalX != CurrentX && OriginalY != CurrentY)
            {
                return false;
            }

            ProgramControl.SetMove(CurrentX, CurrentY, OriginalX, OriginalY, Matrix);

            return true;
        }
    }
}
