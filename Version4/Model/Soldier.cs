using System;
using Model;

namespace Control
{
    class Soldier : Chess
    {
        public override bool ChessMovingRule(int CurrentX, int CurrentY, int OriginalX, int OriginalY, Chess[,] Matrix)
        {
            //ProgramControl con = new ProgramControl();
            //不能吃掉自己的棋子，也就是两方的side不能一样                                   //小兵的X坐标和Y坐标不能同时发生改变 不能斜着走
            if (Matrix[OriginalX, OriginalY].side == Matrix[CurrentX, CurrentY].side || (CurrentX != OriginalX && CurrentY != OriginalY))
            {
                return false;
            }
            //判断红黑方 
            if (Matrix[OriginalX, OriginalY].side == Chess.Player.red)
            {   //没过河
                if (OriginalX > 4 && OriginalX - CurrentX != 1) //过河后不能往后退，只能向前移动一格
                {
                    return false;
                }
                //过了河
                if (OriginalX < 5)
                {
                    //兵一次只能向左或右移动一个格子
                    if (CurrentX == OriginalX && Math.Abs(CurrentY - OriginalY) != 1)
                    {
                        return false;
                    }
                    //兵只能向前走一个格子
                    if (CurrentY == OriginalY && OriginalX - CurrentX != 1)
                    {
                        return false;
                    }
                }
            }
            else if (Matrix[OriginalX, OriginalY].side == Chess.Player.black)
            {   //位置为黑方，也即选中的棋子是黑方小兵，棋盘上方

                //chozen x<10 小兵不能往回走 一次不能往前走两个格 ==2说明走了一个格子
                //没过河
                if (OriginalX < 5 && CurrentX - OriginalX != 1)
                {
                    return false;
                }
                //过了河
                if (OriginalX > 4)
                {
                    //兵一次只能向左或右移动一个格子
                    if (CurrentX == OriginalX && Math.Abs(CurrentY - OriginalY) != 1)
                    {
                        return false;
                    }
                    //兵只能向前走一个格子
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