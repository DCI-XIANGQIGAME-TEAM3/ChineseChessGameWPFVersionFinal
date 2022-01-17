using System;
using Model;

namespace Control
{
    class General : Chess
    {
        public override bool ChessMovingRule(int CurrentX, int CurrentY, int OriginalX, int OriginalY, Chess[,] Matrix)
        {
            int min, max, k;
            // flying general
            if (Matrix[CurrentX, CurrentY].type == Chess.Piecetype.jiang && OriginalY == CurrentY)
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

                for (k = min + 1; k < max; k++) // Are there any chess between the two generals
                {
                    // You cannot fly when there are chess between two generals
                    if (Matrix[k, CurrentY].side != Chess.Player.blank)
                    {
                        return false;
                    }
                }

                // In order to avoid being restricted by the following conditions when flying
                ProgramControl.SetMove(CurrentX, CurrentY, OriginalX, OriginalY, Matrix);

                return true;
            }

            // Cannot capture chess from the same side
            if (Matrix[OriginalX, OriginalY].side == Matrix[CurrentX, CurrentY].side)
            {
                return false;
            }

            // General cannot move out of middle of "田"
            if (Matrix[OriginalX, OriginalY].side == Chess.Player.red)
            {
                if (CurrentY < 3 || CurrentY > 5 || CurrentX < 7)
                {
                    return false;
                }
            }
            else if (Matrix[OriginalX, OriginalY].side == Chess.Player.black)
            {
                if (CurrentY < 3 || CurrentY > 5 || CurrentX > 2)
                {
                    return false;
                }
            }

            // It can only move horizontally or vertically ,and move one unit
            if (Math.Abs(OriginalX - CurrentX) + Math.Abs(OriginalY - CurrentY) != 1)
            {
                return false;
            }

            ProgramControl.SetMove(CurrentX, CurrentY, OriginalX, OriginalY, Matrix);

            return true;
        }
              
    }
}
