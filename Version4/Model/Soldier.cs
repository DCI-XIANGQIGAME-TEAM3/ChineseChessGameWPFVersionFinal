using System;
using Model;

namespace Control
{
    class Soldier : Chess
    {
        public override bool ChessMovingRule(int CurrentX, int CurrentY, int OriginalX, int OriginalY, Chess[,] Matrix)
        {
            //can't eat your own chess pieces                                           //The X and Y coordinates cannot be changed at the same time
            if (Matrix[OriginalX, OriginalY].side == Matrix[CurrentX, CurrentY].side || (CurrentX != OriginalX && CurrentY != OriginalY))
            {
                return false;
            }
            //Judge red and black side 
            if (Matrix[OriginalX, OriginalY].side == Chess.Player.red)
            {   //not cross the river
                if (OriginalX > 4 && OriginalX - CurrentX != 1) //You can't go back after crossing the river. You can only move forward one space
                {
                    return false;
                }
                //cross the river
                if (OriginalX < 5)
                {
                    //Soldiers can only move one grid left or right at a time
                    if (CurrentX == OriginalX && Math.Abs(CurrentY - OriginalY) != 1)
                    {
                        return false;
                    }
                    //Soldiers can only move forward one grid
                    if (CurrentY == OriginalY && OriginalX - CurrentX != 1)
                    {
                        return false;
                    }
                }
            }
            else if (Matrix[OriginalX, OriginalY].side == Chess.Player.black)
            {   
                //not cross the river
                if (OriginalX < 5 && CurrentX - OriginalX != 1)
                {
                    return false;
                }
                //cross the river
                if (OriginalX > 4)
                {
                    //Soldiers can only move one grid left or right at a time
                    if (CurrentX == OriginalX && Math.Abs(CurrentY - OriginalY) != 1)
                    {
                        return false;
                    }
                    //Soldiers can only move forward one grid
                    if (CurrentY == OriginalY && CurrentX - OriginalX != 1)
                    {
                        return false;
                    }
                }

            }

                ProgramControl.SetMove(CurrentX, CurrentY, OriginalX, OriginalY, Matrix);

            return true;
        }
    }
}
