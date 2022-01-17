using System;
using Model;

namespace Control
{
    class Advisor : Chess
    {

        public override bool ChessMovingRule(int CurrentX, int CurrentY, int OriginalX, int OriginalY, Chess[,] Matrix)
        {
            // Move one unit at a time, move diagonally            
            if (Math.Abs(CurrentX - OriginalX) != 1 || Math.Abs(OriginalY - CurrentY) != 1)
            {
                return false;
            }
            // Cannot capture chess from the same side
            if (Matrix[OriginalX, OriginalY].side == Matrix[CurrentX, CurrentY].side)
            {
                return false;
            }

            // advisor cannot move out of "田"
            if (Matrix[OriginalX, OriginalY].side == Chess.Player.red)
            {
                if (CurrentY < 3 || CurrentY > 5 || CurrentX < 7)  // red
                {
                    return false;
                }
            }
            else if (Matrix[OriginalX, OriginalY].side == Chess.Player.black)
            {
                if (CurrentY < 3 || CurrentY > 5 || CurrentX > 2)  // black
                {
                    return false;
                }
            }

            ProgramControl.SetMove(CurrentX, CurrentY, OriginalX, OriginalY, Matrix);

            return true;
        }
    }
}
