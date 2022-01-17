using System;
using Model;

namespace Control
{
    class Advisor : Chess
    {

        public override bool ChessMovingRule(int CurrentX, int CurrentY, int OriginalX, int OriginalY, Chess[,] Matrix)
        {
            //一次只能移动一格
            if (Math.Abs(CurrentX - OriginalX) != 1 || Math.Abs(OriginalY - CurrentY) != 1)
            {
                return false;
            }
            //不能吃自己的棋子
            if (Matrix[OriginalX, OriginalY].side == Matrix[CurrentX, CurrentY].side)
            {
                return false;
            }

            //士不能出田字格
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

            ProgramControl.SetMove(CurrentX, CurrentY, OriginalX, OriginalY, Matrix);

            return true;
        }
    }
}