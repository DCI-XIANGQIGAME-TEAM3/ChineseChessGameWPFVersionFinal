using System;
using Model;

namespace Control
{
    class Chariot : Chess
    {
        public override bool ChessMovingRule(int CurrentX, int CurrentY, int OriginalX, int OriginalY, Chess[,] Matrix)
        {
            //ProgramControl con = new ProgramControl();
            int min, max; //use min to record the small coordinate and max to record the big coordinate
            //如果是Y坐标发生改变
            if (OriginalX == CurrentX)
            {
                if (OriginalY < CurrentY) // 向右移动
                {
                    //如果原来的y小于移动后的y i就等于原来的y
                    min = OriginalY;
                    max = CurrentY;
                }
                else //向左移动
                {
                    //如果原来的y大于移动后的y j就等于原来的y
                    min = CurrentY;
                    max = OriginalY;
                    // 右移（原来的y小于现在的）i原来的y，j现在的y
                }
                //原来的和现在的y坐标之间不能有棋子 
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
                if (OriginalX < CurrentX) //向下移动
                {
                    min = OriginalX;
                    max = CurrentX;
                }
                else //向上移动
                {
                    min = CurrentX;
                    max = OriginalX;
                }
                //原来的和现在的y坐标之间不能有棋子
                for (int i = min + 1; i < max; i++)
                {
                    if (Matrix[i, CurrentY].side != Chess.Player.blank)
                    {
                        return false;
                    }
                }

            }
                //不能吃掉自己的棋子
                if (Matrix[OriginalX, OriginalY].side == Matrix[CurrentX, CurrentY].side)
            {
                return false;
            }
            //不能X坐标和Y坐标同时发生改变
            if (OriginalX != CurrentX && OriginalY != CurrentY)
            {
                return false;
            }

            ProgramControl.SetMove(CurrentX, CurrentY, OriginalX, OriginalY, Matrix);

            return true;
        }
    }
}