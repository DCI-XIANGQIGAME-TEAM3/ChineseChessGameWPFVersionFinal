using System;
using Model;

namespace Control
{
    class Elephant : Chess
    {
        public override bool ChessMovingRule(int CurrentX, int CurrentY, int OriginalX, int OriginalY, Chess[,] Matrix)
        {
            int centerX = (CurrentX + OriginalX) / 2;
            int centerY = (CurrentY + OriginalY) / 2;
            //确保路线是田字路线
            if (Math.Abs(CurrentX - OriginalX) != 2 || Math.Abs(CurrentY - OriginalY) != 2)
            {
                return false;
            }
            //田字格的中心不能有棋子
            if (Matrix[centerX, centerY].side != Chess.Player.blank)
            {
                return false;
            }


            // 不能吃掉自己方的棋子
            if (Matrix[OriginalX, OriginalY].side == Matrix[CurrentX, CurrentY].side)
            {
                return false;
            }

            // 象不能过河
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