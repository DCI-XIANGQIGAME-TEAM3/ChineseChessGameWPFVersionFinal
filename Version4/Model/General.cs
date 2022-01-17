using System;
using Model;

namespace Control
{
    class General : Chess
    {
        public override bool ChessMovingRule(int CurrentX, int CurrentY, int OriginalX, int OriginalY, Chess[,] Matrix)
        {
            int min, max, k;
            //飞将军
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

                for (k = min + 1; k < max; k++)
                {
                    //两个将军之间有棋子时不能飞将
                    if (Matrix[k, CurrentY].side != Chess.Player.blank)
                    {
                        return false;
                    }
                }

                //为了在飞将时避免被下面的条件限制
                ProgramControl.SetMove(CurrentX, CurrentY, OriginalX, OriginalY, Matrix);

                return true;
            }

            //  不能吃自己的棋子
            if (Matrix[OriginalX, OriginalY].side == Matrix[CurrentX, CurrentY].side)
            {
                return false;
            }

            // 将不能出田字格
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

            //只能水平移动或是竖着移动
            if (Math.Abs(OriginalX - CurrentX) + Math.Abs(OriginalY - CurrentY) != 1)
            {
                return false;
            }

            ProgramControl.SetMove(CurrentX, CurrentY, OriginalX, OriginalY, Matrix);

            return true;
        }
              
    }
}