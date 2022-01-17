using System;
using Model;

namespace Control
{
    class Elephant : Chess
    {
        public override bool ChessMovingRule(int CurrentX, int CurrentY, int OriginalX, int OriginalY, Chess[,] Matrix)
        {   
            // The center point of the "田"
            int centerX = (CurrentX + OriginalX) / 2;
            int centerY = (CurrentY + OriginalY) / 2;
            // Elephant moves two units at the same time in the X and Y coordinates
            if (Math.Abs(CurrentX - OriginalX) != 2 || Math.Abs(CurrentY - OriginalY) != 2)
            {
                return false;
            }
            // The center of the "田" cannot have a chess
            if (Matrix[centerX, centerY].side != Chess.Player.blank)
            {
                return false;
            }


            // Cannot capture chess from the same side
            if (Matrix[OriginalX, OriginalY].side == Matrix[CurrentX, CurrentY].side)
            {
                return false;
            }

            // Elephant can't cross the river
            if (Matrix[OriginalX, OriginalY].side == Chess.Player.red)
            {
                if (CurrentX < 5)
                {
                    return false;
                }
            }
            else if (Matrix[OriginalX, OriginalY].side == Chess.Player.black)
            {
                if (CurrentX > 4)
                {
                    return false;
                }
            }


            ProgramControl.SetMove(CurrentX, CurrentY, OriginalX, OriginalY, Matrix);

            return true;
        }
    }
}
