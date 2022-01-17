using System;
using Model;


namespace Control
{
    class Cannon : Chess
    {
        public override bool ChessMovingRule(int CurrentX, int CurrentY, int OriginalX, int OriginalY, Chess[,] Matrix)
        {
            int min, max, k, num=0;
            //不能斜着走 X,Y坐标不能同时改变
            if (CurrentX != OriginalX && CurrentY != OriginalY)
            {
                return false;
            }

            if (OriginalX == CurrentX)
            {
                if (OriginalY < CurrentY)
                {
                    //如果原来的y小于移动后的y i就等于原来的y
                    min = OriginalY;
                    max = CurrentY;
                }
                else
                {
                    //如果原来的y大于移动后的y j就等于原来的y
                    min = CurrentY;
                    max = OriginalY;
                    // 右移（原来的y小于现在的）i原来的y，j现在的y
                }
                num = 0;
                //n就是他原来位置和现在位置中间的棋子数量
                for (k = min + 1; k < max; k++)
                {
                    if (Matrix[CurrentX, k].side != Chess.Player.blank)
                    {
                        num++;
                    }
                }
            }
            else if (OriginalY == CurrentY)
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

            //如果中间的棋子数量大于一个的话 说明不能移动
            if (num > 1)
            {
                return false;
            }
            //CurrentX,CurrentY  为移动后的坐标 中间没有棋子且移动后的坐标不是空的
            if (num == 0 && Matrix[CurrentX, CurrentY].side != Chess.Player.blank)
            {
                return false;
            }
            //中间有一个棋子，且移动后的坐标是空的
            if (num == 1 && Matrix[CurrentX, CurrentY].side == Chess.Player.blank)
            {
                return false;
            }
            //不能吃掉自己方的棋子
            if (Matrix[OriginalX, OriginalY].side == Matrix[CurrentX, CurrentY].side)
            {
                return false;
            }

            ProgramControl.SetMove(CurrentX, CurrentY, OriginalX, OriginalY, Matrix);

            return true;
        }

    }
}